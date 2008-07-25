#********************************************************************#
# KPC_Ruby require fxruby gem version 1.6.11 or greater, which as of #
# Ruby 1.8.5 release, must be installed separately.                  #
#                                                                    #
# @author Nathan Lane                                                #
# @date 08/14/2007                                                   #
#                                                                    #
# This script is based on the original KPC program designed by Noah  #
# Rankins. KPC was originally developed in Java. The original KPC    #
# project is located on http://kpc.sourceforge.net/                  #
#                                                                    #
# * Original CLI written by Noah Rankins.                            #
# * Original Swing UI designed and written by Nathan Lane and        #
#   adapted to KPC's event model by Noah Rankins.                    #
# * Regards also to the KPC Team.                                    #
#********************************************************************#

$VERBOSE = nil # Turn off verbose display of Ruby messages, because FXRuby has a lot

# Here is my KPC GUI
class KPCGUI
	
	require "fox16" # Use the latest FXRuby version 1.6

	include Fox # Include the Fox module

	def initialize()
		@kpcClient = KPC.new
		@kpcMsgHandler = KPCMessageHandler.new
		
		# Create a new Fox application
		@application = FXApp.new
		@connectionString = ""
		@connectToDialog = FXInputDialog.new(@application, "Connect To", nil, :opts => (DECOR_NONE|PLACEMENT_OWNER))
		
		setup_fox_app
	end # End: def initialize()
	
	def setup_fox_app()
		# Application icon
		helloAppIconFile = File.open("logo.png", "rb")
		@helloAppIcon = FXPNGIcon.new(@application, helloAppIconFile.read, 0, 0, 16, 16)
		
		# Main program window
		@mainWindow = FXMainWindow.new(@application, "KPC - Ruby", @helloAppIcon, nil, DECOR_ALL, 20, 40, 640, 500)
		
		# Setup the GUI
		setup_fox_app_gui
		# Attach event handlers to GUI components
		attach_event_handlers
		
		# Create the application - this is a packaging process
		@application.create
	end # End: def setup_fox_app()
	
	def setup_fox_app_gui()
		# Menu bar
		@menubar = FXMenuBar.new(@mainWindow, LAYOUT_SIDE_TOP|LAYOUT_FILL_X)
		# File menu
		@fileMenu = FXMenuPane.new(@mainWindow)
		FXMenuTitle.new(@menubar, "&File", nil, @fileMenu)
		# Connect menu item
		@fileMenuConnect = FXMenuCommand.new(@fileMenu, "&Connect")
		# Separator
		FXMenuSeparator.new(@fileMenu)
		# Exit menu item
		@fileMenuExit = FXMenuCommand.new(@fileMenu, "E&xit")
		
		# Split pane
		@splitter = FXSplitter.new(@mainWindow, (LAYOUT_SIDE_TOP|LAYOUT_FILL_X|LAYOUT_FILL_Y|SPLITTER_REVERSED|SPLITTER_TRACKING))
		@group1 = FXVerticalFrame.new(@splitter, (FRAME_NORMAL|LAYOUT_FILL_X|LAYOUT_FILL_Y), :padding => 0)
		@group2 = FXVerticalFrame.new(@splitter, (FRAME_NORMAL|LAYOUT_FILL_X|LAYOUT_FILL_Y))
		
		# Vertical frame for text part
		@verticalTextFrame = FXVerticalFrame.new(@group1, (LAYOUT_FILL_X|LAYOUT_FILL_Y))
		
		# Text Area
		@textArea = FXText.new(@verticalTextFrame, :opts => (LAYOUT_FILL_X|LAYOUT_FILL_Y|FRAME_SUNKEN|FRAME_NORMAL|TEXT_READONLY|TEXT_WORDWRAP|TEXT_AUTOSCROLL))
		
		# Horizontal frame for text field and button
		@horizontalTextButtonFrame = FXHorizontalFrame.new(@verticalTextFrame, (LAYOUT_FILL_X))
		
		# Text Field
		@textField = FXTextField.new(@horizontalTextButtonFrame, 30, :opts => (LAYOUT_SIDE_BOTTOM|LAYOUT_FILL_Y|LAYOUT_FILL_X|FRAME_NORMAL|TEXTFIELD_ENTER_ONLY))
		
		# Button to send
		@sendButton = FXButton.new(@horizontalTextButtonFrame, "Send")
		
		# Vertical frame for list area
		@verticalListFrame = FXVerticalFrame.new(@group2, (LAYOUT_FILL_X|LAYOUT_FILL_Y))
		
		# Label above contact list
		@contactListLabel = FXLabel.new(@verticalListFrame, "Clients Available", :opts => (LAYOUT_FILL_X|JUSTIFY_CENTER_X|JUSTIFY_CENTER_Y), :padLeft => 50, :padRight => 50)
		
		# List for Contacts
		@contactList = FXList.new(@verticalListFrame, :opts => (LIST_EXTENDEDSELECT|LAYOUT_FILL_X|LAYOUT_FILL_Y))
	end # End: def setup_fox_app_gui()
	
	def attach_event_handlers()
		@fileMenuConnect.connect(SEL_COMMAND) do |sender, selector, data|
			result = @connectToDialog.execute
			
			if(result == 1)
				@connectionString = @connectToDialog.text
				if(@kpcClient.add_client(@connectionString))
					@contactList.appendItem(@connectionString)
				end # End: if(@kpcClient.add_client(@connectionString))
			else
				@connectionString = ""
			end # End: if(result == 1)
		end # End: @fileMenuConnect.connect(SEL_COMMAND) do |sender, selector, data|
		
		@textField.connect(SEL_COMMAND) do |sender, selector, data|
			if(not @textField.text == "" and @kpcClient.connected?)
				@kpcClient.send_message(@textField.text)
				@textArea.appendText(@kpcClient.userName + "@" + @kpcClient.networkAddress + ": " + @textField.text + "\n")
				@textField.text = ""
			end # End: if(not @textField.text == "")
		end # End: @textField.connect(SEL_COMMAND) do |sender, selector, data|
		
		@sendButton.connect(SEL_COMMAND) do |sender, selector, data|
			if(not @textField.text == "" and @kpcClient.connected?)
				@kpcClient.send_message(@textField.text)
				@textArea.appendText(@kpcClient.userName + "@" + @kpcClient.networkAddress + ": " + @textField.text + "\n")
				@textField.text = ""
			end # End: if(not @textField.text == "")
		end # End: @sendButton.connect(SEL_COMMAND) do |sender, selector, data|
		
		@fileMenuExit.connect(SEL_COMMAND) do |sender, selector, data|
			# Stop the message handler
			@kpcMsgHandler.stop_kpc_message_handler
			@localMsgThread.terminate
			# Exit KPC
			exit
		end # End: @fileMenuExit.connect(SEL_COMMAND) do |sender, selector, data|
		
		@mainWindow.connect(SEL_CLOSE) do |sender, selector, data|
			# Stop the message handler
			@kpcMsgHandler.stop_kpc_message_handler
			@localMsgThread.terminate
			# Exit KPC
			exit
		end # End: @mainWindow.connect(SEL_COMMAND) do |sender, selector, data|
	end # End: def attach_event_handlers()
	
	def run_kpc_gui()
		# Start the message handler
		@kpcMsgHandler.start_kpc_message_handler
		# Create a new thread to handle messages for the GUI
		@localMsgThread = Thread.start do
			while(true)
#				puts "KPCGUI - Message Queue Size: " + @kpcMsgHandler.messageQueue.length.to_s
				while(@kpcMsgHandler.messageQueue.length > 0)
					info, msg = @kpcMsgHandler.get_next_message
					msg = msg + "\n"
					clientName = @kpcClient.find_client(info)
					
					if(clientName.nil?)
						clientName = "Unknown"
					end # End: if(clientName.nil?)
					
#					puts "Message: " + msg.to_s
					if(not msg.nil?)
						@textArea.appendText(clientName + ": " + msg.to_s)
#						puts "Appended Message"
					end # End: if(not msg.nil?)
				end # End: while(@kpcMsgHandler.messageQueue.length > 0)
				sleep(1)
			end # End: while(true)
		end # End: @localMsgThread = Thread.start do
		# Show the window
		@mainWindow.show
		# Run the program loop
		@application.run
	end # End: def run_kpc_gui()
	
end # End: class KPCGUI

class KPC
	
	require "socket"
	
	attr_reader :hostName, :networkAddress, :clientAddressList, :port, :userName, :msgLength
	
	def initialize(strUserName = nil)
		@port = 4181 # The KPC server port
		@msgLength = 1024 # The KPC standard message length
		@hostName = Socket.gethostname
		@networkAddress = IPSocket.getaddress(@hostName)
		#@clientserver = UDPSocket.new # This is a placeholder more or less, we'll use open.send later
		@clientAddressList = Array.new
		@connected = false
		
		if(strUserName.nil?)
			@userName = @hostName
		end # End: if(userName.nil?)
		
		setup_command_table
	end # End: def initialize()
	
	def setup_command_table()
		@commandHash = Hash.new
		# Command table - for CLI
	end # End: def setup_command_table()
	
	def add_client(strClientIP)
#		puts "Adding client: " + strClientIP.to_s
		retVal = false
		
		if(@clientAddressList.inspect[strClientIP].nil?)
			@clientAddressList << strClientIP
			
			if(not @connected)
				@connected = true
			end # End: if(not @connected)
			
			retVal = true
		end # End: if(@clientAddressList.inspect[strClientIP].nil?)
		
		return retVal
	end # End: def add_client(strClientIP)
	
	def find_client(strClientInfo)
		retVal = nil
		
		@clientAddressList.each do |clientIP|
			if(not strClientInfo[clientIP].nil?)
				retVal = clientIP
			end # End: if(not strClientInfo[clientIP].nil?)
		end # End: @clientAddressList.each do |clientIP|
		
		return retVal
	end # End: def find_client(strClientIP)
	
	def send_message(strMessage)
#		puts "Sending message: " + strMessage.to_s
		if(not @clientAddressList.empty?)
			@clientAddressList.each do |clientAddress|
				UDPSocket.open.send(strMessage, 0, clientAddress, @port)
			end # End: @clientAddressList.each do |clientAddress|
		end # End: if(not @clientAddressList.empty?)
	end # End: def send_message(strMessage)
	
	def connected?()
		return @connected
	end # End: def connected?()
	
end # End: class KPC

class KPCMessageHandler
	
	require "socket"
	
	attr_reader :messageQueue
	
	def initialize(intPort = 4181, intMessageLength = 1024)
		@port = intPort
		@msgLength = intMessageLength
		@messageQueue = Array.new
	end # End: def initialize()
	
	def start_kpc_message_handler()
#		puts "Starting message handler"
		@kpcMsgThread = Thread.start do
			@server = UDPSocket.open
			@server.bind(nil, @port)
			while(true)
				@data, @senderInfo = @server.recvfrom(@msgLength)
				@messageQueue << [@senderInfo.to_s, @data.to_s]
				sleep(1)
			end # End: while(true)
		end # End: @kpcMsgThread = Thread.start do
	end # End: def start_kpc_message_handler()
	
	def stop_kpc_message_handler()
#		puts "Stopping message handler"
		if(not @kpcMsgThread.nil?)
			@kpcMsgThread.terminate
		end # End: if(not @kpcMsgThread.nil?)
	end # End: def stop_kpc_message_handler()
	
	def get_next_message()
#		puts "getting next message: " + @messageQueue[0].to_s
		retVal = nil
		
		retVal = @messageQueue.delete_at(0)
		
		return retVal
	end # End: def get_next_message()
	
end # End: class KPCMessageHandler

# Script entry point
if __FILE__ == $0
	kpcGUI = KPCGUI.new # Create new application
	kpcGUI.run_kpc_gui # Run the application
end # End: if __FILE__ == $0