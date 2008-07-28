package spidertool;

import javax.swing.*;
import java.net.*;
import java.awt.*;
import java.awt.event.*;
import java.io.*;
import java.util.*;

public class SpiderTool extends JFrame implements ActionListener, KeyListener {
	
	public static final long serialVersionUID = 0L;
	
	private JTextArea outputArea;
	private JTextField inputField;
	private URL url;
	private String strURL;
	private HttpURLConnection connection;
	private ArrayList commandHistory;
	private ArrayList contentLines;
	private int hCounter;
	private boolean areConnected, interactionEnabled, redirectionEnabled, fileLoaded;
	private InputStream is;
	private InputStreamReader r;
	private BufferedReader br;
	private File f;
	private byte[] fileStream;
	
	private String[] dictionary = {
		"conn",
		"get",
		"click",
		"?",
		"help",
		"h",
		"find",
		"close",
		"clear",
		"cls",
		"exit",
		"dump",
		"history",
		"reconnect",
		"autoredirect",
		"interact",
		"load",	// Fs command
		"read"	// Fs command
	};
	
	private boolean[] arguments = {
			true,	// conn
			true,	// get
			true,	// click
			false,	// ?
			false,	// help
			false,	// h
			true,	// find
			false,	// close
			false,	// clear
			false,	// cls
			false,	// exit
			false,	// dump
			false,	// history
			false,	// reconnect
			false,	// autoredirect
			false,	// interact
			true,	// load
			false	// read
	};
	
	public SpiderTool() {
		super("Spider Tool");
		
		setupUI();
		commandHistory = new ArrayList();
		hCounter = 0;
		areConnected = false;
		interactionEnabled = false;
		redirectionEnabled = false;
		fileLoaded = false;
	}
	
	private void setupUI() {
		setSize(640, 480);
		setLayout(new BorderLayout());
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		
		outputArea = new JTextArea();
		outputArea.setEditable(false);
		outputArea.setFocusable(false);
		outputArea.setBackground(Color.BLACK);
		outputArea.setForeground(Color.GREEN);
		outputArea.setWrapStyleWord(true);
		outputArea.setLineWrap(true);
		outputArea.setFont(new Font("Lucida Console", Font.PLAIN, 12));
		JScrollPane jsp = new JScrollPane(outputArea);
		jsp.setAutoscrolls(true);
		getContentPane().add(jsp, BorderLayout.CENTER);
		
		inputField = new JTextField();
		inputField.setBackground(Color.BLACK);
		inputField.setForeground(Color.GREEN);
		inputField.setCaretColor(Color.GREEN);
		inputField.addActionListener(this);
		inputField.addKeyListener(this);
		getContentPane().add(inputField, BorderLayout.SOUTH);
		
		ImageIcon appIcon = new ImageIcon("bug.png");
		setIconImage(appIcon.getImage());
		
		setVisible(true);
		inputField.requestFocus();
	}
	
	public static void main(String args[]) {
		new SpiderTool();
	}
	
	public void actionPerformed(ActionEvent ae) {		
		String eventString = ((JTextField)ae.getSource()).getText();
		
		eventString = eventString.toLowerCase();
		
		commandHistory.add(eventString);
		hCounter = commandHistory.size();
		
		String[] eventRequests = eventString.split(";");
		System.out.println("DEBUG: " + eventRequests.length + " requests made");
		boolean commandKnown = false;
		for(int er = 0; er < eventRequests.length; ++er) {
			String[] tokens = eventRequests[er].split("->");
			
			for(int j = 0; j < dictionary.length; ++j) {
				if(tokens[0].equals(dictionary[j])) {
					outputArea.append("$> " + eventRequests[er] + "\n");
					commandKnown = true;
					if((arguments[j] && tokens.length > 1) || !arguments[j]) {
						try {
							((JTextField)ae.getSource()).setEnabled(false);
							outputArea.setCursor(new Cursor(Cursor.WAIT_CURSOR));
							parseAction(eventRequests[er]);
						} catch(MalformedURLException mfue) {
							outputArea.append("Malformed URL exception was caught: " + (eventString.split("->"))[1] + "\n");
							outputArea.append("! Be sure to include the protocol in your URL, for example http:// or ftp://\n");
							outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
							mfue.printStackTrace();
						} catch(IOException ioe) {
							outputArea.append("An exception occured while attempting to connect to " + (eventString.split("->"))[1] + "\n");
							outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
							ioe.printStackTrace();
						} catch(ArrayIndexOutOfBoundsException ioobe) {
							outputArea.append("No URL was provided for conn->\n");
							outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
							ioobe.printStackTrace();
						} catch(InterruptedException ie) {
							outputArea.append("Unable to parse for for output\n");
							outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
							ie.printStackTrace();
						}
					} else {
						if(arguments[j]) {
							outputArea.append(tokens[0] + " requires at least one argument using the -> operator\n");
						} else {
							outputArea.append(tokens[0] + " requires zero arguments\n");
						}
					}
					
					outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
					((JTextField)ae.getSource()).setEnabled(true);
				}
			}
		}
		
		if(!commandKnown) {
			outputArea.append("Invalid command: " + eventString + "\n");
		}
		
		((JTextField)ae.getSource()).setText("");
	}
	
	private void parseAction(String eventString) throws MalformedURLException, IOException, InterruptedException {
		String[] tokens = eventString.split("->");
		System.out.println("DEBUG: " + "event string size = " + tokens.length);
		if(tokens[0].equals("conn")) {
			System.out.println("Attempting to connect to URL");
			strURL = tokens[1];
			injectURLShortcuts(strURL);

			contentLines = null;
			url = new URL(strURL);
			connection = (HttpURLConnection)url.openConnection();

			connection.connect();
			outputArea.append("conn: Successful connection was made with " + strURL + "\n");
			areConnected = true;
			setTitle("Spider Tool - " + strURL);

			is = connection.getInputStream();
			r = new InputStreamReader(is);
			br = new BufferedReader(r);
		} else
		if(tokens[0].equals("h") || tokens[0].equals("help") || tokens[0].equals("?")) {
			System.out.println("Providing help");
			outputArea.append("Available commands:\n");
			provideHelp(eventString);
		} else
		if(tokens[0].equals("get")) {
			if(!areConnected) {
				outputArea.append("You are not currently connected to a site: run conn->{url} first\n");
			} else {
				if(tokens[1].equals("type")) {
					System.out.println("Getting type");
					String contentType = connection.getContentType();
					outputArea.append("Content Type: " + contentType + "\n");
				} else
				if(tokens[1].equals("content")) {
					inputField.setCursor(new Cursor(Cursor.WAIT_CURSOR));
					System.out.println("Getting content");
					
					String line = null;
					contentLines = new ArrayList();
					do {
						line = br.readLine();
						if(line != null) {
							// Keep lines
							System.out.println(line);
							contentLines.add(line);
						}
					} while(line != null);
					
					outputArea.append("Spider Tool read " + contentLines.size() + " total lines from " + url.getPath() + "\n");
					inputField.setCursor(new Cursor(Cursor.TEXT_CURSOR));					
				} else
				if(tokens[1].equals("stream")) {
					outputArea.setCursor(new Cursor(Cursor.WAIT_CURSOR));
					System.out.println("Getting stream");
					int length = connection.getContentLength();
					
					char[] contentStream = new char[length];
					r.read(contentStream);
					
					String strContentStream = new String(contentStream);
					byte[] byteStream = strContentStream.getBytes();
					
					JFileChooser jfc = new JFileChooser();
					int result = jfc.showSaveDialog(this);
					if(result == JFileChooser.APPROVE_OPTION) {
						File f = jfc.getSelectedFile();
						FileOutputStream fos = new FileOutputStream(f);
						fos.write(byteStream);
						fos.close();
					}
					
					outputArea.append("Spider Tool read " + length + " total bytes from " + url.getPath() + "\n");
					outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
				} else
				if(tokens[1].equals("header")) {
					if(tokens.length == 3) {
						String strField = connection.getHeaderField(tokens[2]);
						outputArea.append("header field " + tokens[2] + " equals " + strField + "\n");
					} else {
						for(int i = 1; ; ++i) {
							String headerField = connection.getHeaderField(i);
							
							if(headerField == null) {
								break;
							}
							
							outputArea.append("header field[" + connection.getHeaderFieldKey(i) + "]: " + headerField + "\n");
						}
					}
				} else
				if(tokens[1].equals("response") || tokens[1].equals("responsecode") || tokens[1].equals("code")) {
					String responseCode = connection.getHeaderField(0);
					outputArea.append("response code equals " + responseCode + "\n");
				} else
				if(tokens[1].equals("autoredirect")) {
					outputArea.append("Automatically redirect = " + redirectionEnabled + "\n");
				} else
				if(tokens[1].equals("interact")) {
					outputArea.append("User interaction enabled = " + interactionEnabled + "\n");
				} else {
					outputArea.append("! " + tokens[1] + " is not a valid argument for get->{a}\n");
				}
			}
		} else
		if(tokens[0].equals("find")) {
			System.out.println("Looking for '" + tokens[1] + "'");
			if(contentLines != null && contentLines.size() > 0) {
				int counter = 0;
				for(int i = 0; i < contentLines.size(); ++i) {
					String line = (String)contentLines.get(i);
					if(line.indexOf(tokens[1]) > -1) {
						outputArea.append("" + (counter + 1) + ": " + line + "\n");
						System.out.println(line);
						++counter;
					}
				}
				outputArea.append("Found " + counter + " lines containing " + tokens[1] + "\n");
			} else {
				outputArea.append("Nothing found to search through: run get->content first\n");
			}
/*			
			if(tokens.length == 3) {
				if(tokens[1].equals("regex")) {
					String regexStr = tokens[2];
				}
			}
*/
		} else
		if(tokens[0].equals("close")) {
			if(areConnected) {
				connection = null;
				areConnected = false;
				contentLines = null;
				is.close();
				r.close();
				br.close();
				outputArea.append("Closed connection to " + url.getHost() + "\n");
				setTitle("Spider Tool");
			} else {
				outputArea.append("No connection to close\n");
			}
		} else
		if(tokens[0].equals("clear") || tokens[0].equals("cls")) {
			if(tokens.length > 1) {
				if(tokens[1].equals("history")) {
					commandHistory = null;
					commandHistory = new ArrayList();
					outputArea.append("Command history was cleared\n");
				} else
				if(tokens[1].equals("screen")) {
					outputArea.setText("");
				}
			} else {
				outputArea.setText("");
			}
		} else
		if(tokens[0].equals("exit")) {
			setVisible(false);
			dispose();
			System.exit(0);
		} else
		if(tokens[0].equals("dump")) {
			if(tokens.length > 1) {
				if(tokens[1].equals("file")) {
					if(fileLoaded && fileStream != null && fileStream.length > 0) {
						for(int i = 0; i < (fileStream.length / 16); ++i) {
							String strS = "";
							String strNum = "" + (i * 16);
							
							if(strNum.length() < 4) {
								String strPad = "";
								for(int k = strNum.length(); k < 4; ++k) {
									strPad += "0";
								}
								
								strNum = strPad + strNum;
							}
							
							outputArea.append(strNum + "\t");
									
							try {
								for(int j = 0; j < 16; j++) {
									String s = "";
									if(fileStream[i * 16 + j] < 0) {
										// Add negative numbers to 256 to get positive - wrap around theory
										s = Integer.toHexString((256 + fileStream[i * 16 + j]));
									} else {
										s = Integer.toHexString((fileStream[i * 16 + j]));
									}
									
									if(s.length() < 2) {
										s = "0" + s;
									}
									
									if(fileStream[i * 16 + j] != 7) {
										System.out.println("S: " + fileStream[i * 16 + j] + ", " + (char)fileStream[i * 16 + j] + ", " + s);
									}
									
									if(fileStream[i * 16 + j] > 31 && fileStream[i * 16 + j] < 127) {
										strS += (char)fileStream[i * 16 + j];
									} else {
										strS += (char)46;
									}									
									
									outputArea.append("" + s + " ");
									Thread.sleep(1);
								}
							} catch(ArrayIndexOutOfBoundsException aioobe) {
								aioobe.printStackTrace();
							}
							
							outputArea.append("\t" + strS + "\n");							
						}
					}
				} else {
					outputArea.append("Invalid argument supplied for dump->{a}\n");
				}
			} else {
				if(contentLines != null && contentLines.size() > 0) {
					int counter = 1;
					for(int i = 0; i< contentLines.size(); ++i) {
						outputArea.append("" + counter + ": " + contentLines.get(i) + "\n");
						++counter;
					}
				} else {
					outputArea.append("Nothing found to dump: run get->content first\n");
				}
			}
		} else
		if(tokens[0].equals("history")) {
			for(int i = 0; i < commandHistory.size(); ++i) {
				if(hCounter == i) {
					outputArea.append(">");
				}
				
				outputArea.append("" + (i + 1) + ": " + commandHistory.get(i) + "\n");
			}
		} else
		if(tokens[0].equals("reconnect") || tokens[0].equals("repost")) {
			try {
				parseAction("conn->" + strURL);
			} catch(MalformedURLException mfue) {
				outputArea.append("Malformed URL exception was caught: " + (eventString.split("->"))[1] + "\n");
				outputArea.append("! Be sure to include the protocol in your URL, for example http:// or ftp://\n");
				outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
				mfue.printStackTrace();
			} catch(IOException ioe) {
				outputArea.append("An exception occured while attempting to connect to " + (eventString.split("->"))[1] + "\n");
				outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
				ioe.printStackTrace();
			} catch(ArrayIndexOutOfBoundsException ioobe) {
				outputArea.append("No URL was provided for conn->\n");
				outputArea.setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
				ioobe.printStackTrace();
			}
		} else
		if(tokens[0].equals("autoredirect")) {
			if(tokens.length == 2) {
				if(tokens[1].equals("true") || tokens[1].equals("t")) {
					HttpURLConnection.setFollowRedirects(true);
					redirectionEnabled = true;
					outputArea.append("Set auto-redirect to true\n");
				} else
				if(tokens[1].equals("false") || tokens[1].equals("f")) {
					HttpURLConnection.setFollowRedirects(false);
					redirectionEnabled = false;
					outputArea.append("Set auto-redirect to false\n");
				} else {
					outputArea.append("Cannot understand autoredirect->" + tokens[1] + "\n");
				}
			} else {
				if(redirectionEnabled) {
					HttpURLConnection.setFollowRedirects(false);
					redirectionEnabled = false;
					outputArea.append("Set auto-redirect to false\n");
				} else {
					HttpURLConnection.setFollowRedirects(true);
					redirectionEnabled = true;
					outputArea.append("Set auto-redirect to true\n");
				}
			}
		} else
		if(tokens[0].equals("interact")) {
			if(!areConnected) {
				outputArea.append("You are not currently connected to a site: run conn->{url} first\n");
			} else {
				if(tokens.length == 2) {
					if(tokens[1].equals("true") || tokens[1].equals("t")) {
						connection.setAllowUserInteraction(true);
						interactionEnabled = true;
						outputArea.append("Set user interaction to true\n");
					} else
					if(tokens[1].equals("false") || tokens[1].equals("f")) {
						connection.setAllowUserInteraction(false);
						interactionEnabled = false;
						outputArea.append("Set user interaction to false\n");
					} else {
						outputArea.append("Cannot understand interact->" + tokens[1] + "\n");
					}
				} else {
					if(interactionEnabled) {
						connection.setAllowUserInteraction(false);
						interactionEnabled = false;
						outputArea.append("Set user interaction to false\n");
					} else {
						HttpURLConnection.setFollowRedirects(true);
						redirectionEnabled = true;
						outputArea.append("Set user interaction to true\n");
					}
				}
			}
		} else
		if(tokens[0].equals("load")) {
			if(tokens[1].equals("|")) {
				JFileChooser jfc = new JFileChooser();
				int result = jfc.showOpenDialog(this);
				if(result == JFileChooser.APPROVE_OPTION) {
					f = jfc.getSelectedFile();
					fileLoaded = true;
					result = -1;
					outputArea.append("Loaded file " + f.getName() + "\n");
				} else {
					// Do nothing
				}
			} else {
				f = new File(tokens[1]);
				if(f.exists()) {
					fileLoaded = true;
					outputArea.append("Loaded file " + f.getName() + "\n");
				} else {
					fileLoaded = false;
					outputArea.append("Could not find file " + f.getName() + "\n");
				}
			}
		} else
		if(tokens[0].equals("read")) {
			if(fileLoaded) {
				fileStream = new byte[((int)(f.length()))];
				FileInputStream fis = new FileInputStream(f);
				int result = fis.read(fileStream);
				outputArea.append("Read result equals " + result + ": this will take approximately " + (((result * 3) / 1000) / 60) + " minutes to dump\n");
				fis.close();
			} else {
				outputArea.append("No file loaded, please run file->{| or filename}\n");
			}
		}
	}
	
	private void injectURLShortcuts(String shortcuttedURL) {
		System.out.println("Inside injectURLShortcuts (" + shortcuttedURL + ")");
		String retVal = shortcuttedURL;
		
		if(retVal.startsWith("_h")) {
			retVal = retVal.replaceFirst("_h", "http://");
		} else		
		if(retVal.startsWith("_f")) {
			retVal = retVal.replaceFirst("_f", "ftp://");
		} else		
		if(retVal.startsWith("-f")) {
			retVal = retVal.replaceFirst("-f", "file:///");
		}
		
		if(retVal.indexOf("_w") > -1) {
			retVal = retVal.replaceFirst("_w", "www.");
		}
			
		strURL = retVal;
	}
	
	public void keyPressed(KeyEvent ke) {
		if(ke.getKeyCode() == KeyEvent.VK_UP) {
			if(hCounter > 0) {
				String c = (String)commandHistory.get(hCounter - 1);
				inputField.setText(c);
				--hCounter;
			}
		} else
		if(ke.getKeyCode() == KeyEvent.VK_DOWN) {
			if(hCounter < commandHistory.size()) {
				String c = (String)commandHistory.get(hCounter);
				inputField.setText(c);
				++hCounter;
			}
		} else
		if(ke.getKeyCode() == KeyEvent.VK_DELETE) {
			inputField.setText("");
		}
	}
	
	public void keyReleased(KeyEvent ke) { }
	public void keyTyped(KeyEvent ke) { }
	
	private void provideHelp(String eventString) {
		String commList = "";
		
		for(int i = 0; i < dictionary.length; ++i) {
			commList += dictionary[i];
			if(arguments[i]) {
				commList += "->";
			}
			commList += " ";
		}
		
		outputArea.append(commList + "\n");
	}
	
/*	private class HTMLParse extends HTMLEditorKit {
		
		public HTMLEditorKit.Parser getParser() {
			return super.getParser();
		}
		
	}
	
	**
	* A HTML parser callback used by this class to detect links
	*
	* @author Jeff Heaton
	* @version 1.0
	*
	protected class Parser extends HTMLEditorKit.ParserCallback {
		
		protected URL base;
		
		public Parser(URL base) {
			this.base = base;
		}
		
		public void handleSimpleTag(HTML.Tag t,	MutableAttributeSet a, int pos) {
			String href = (String)a.getAttribute(HTML.Attribute.HREF);
			
			if((href == null) && (t == HTML.Tag.FRAME)) {
				href = (String)a.getAttribute(HTML.Attribute.SRC);
			}
			
			if(href == null) {
				return;
			}
			
			int i = href.indexOf('#');
			
			if(i != -1) {
				href = href.substring(0,i);
			}
				
			if(href.toLowerCase().startsWith("mailto:")) {
				// Found mailto
				return;
			}
			
			handleLink(base,href);
		}
		
		public void handleStartTag(HTML.Tag t, MutableAttributeSet a, int pos) {
			handleSimpleTag(t,a,pos); // handle the same way
		}
		
		protected void handleLink(URL base, String str) {
			try {
				URL url = new URL(base, str);
				
				//if(report.spiderFoundURL(base, url)) {
				//	addURL(url);
				//}
			} catch(MalformedURLException e) {
				log("Found malformed URL: " + str );
			}
		}
		
		public void log(String entry) {
			System.out.println((new Date()) + ": " + entry);
		}
	}*/
}
