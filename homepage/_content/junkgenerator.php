<?php
	if($_POST["numberOfBytes"] && $_POST["numberOfBytes"] != "")
	{
?>
<div id="introductionTextContainer" class="innerContainer">
<?php
		// Generate junk and display it.
		
		// Figure out the format using regular expressions, if it's a specific format, use it, otherwise, do something funky.
		//$engineeringNotationRegex = "^[\\d]+(E|e){1}(-|\\+){1}[\\d]+$";
		
		//if(preg_match($engineeringNotationRegex, $_POST["numberOfBytes"])
		//{
		//}
		$allowedBytes = array(
			"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
			"n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
			"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
			"N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "`", "~", "!",
			"#", "$", "%", "^", "&", "8", "(", ")", "-", "_", "=", "+", "{",
			"}", "[", "]", ";", ":", "\"", "'", ",", "<", ">", ".", "/", "?"
		);
		$numberOfBytes = intval($_POST["numberOfBytes"]);
		
		if($numberOfBytes > PHP_INT_MAX)
		{
			$numberOfBytes = PHP_INT_MAX;
		}
		
		$bytes = "";
		
		for($i = 0; $i < $numberOfBytes; $i++)
		{
			$bytes = $bytes . $allowedBytes[rand(0, (sizeof($allowedBytes)))];
		}
?>
	Generated text is <?php echo $numberOfBytes; ?> bytes long.
</div>
<div class="hrDiv"></div>
<div id="junkGeneratorFormContainer" class="junkGeneratorFormContainer">
	<form id="junkGeneratorForm" name="junkGeneratorForm" action="/?page=junkgenerator" method="post">
		<input id="numberOfBytes" name="numberOfBytes" type="hidden" value="<?php echo $_POST["numberOfBytes"]; ?>"/>
		<textarea id="contentJunk" name="contentJunk" class="contentJunk" value="">
<?php
	echo $bytes;
?>
		</textarea>
		<div>
			<button id="junkGeneratorClearButton" name="junkGeneratorClearButton" type="button" value="button" onclick="javascript: location.href = '/?page=junkgenerator';">Clear</button>
			<button id="junkGeneratorSubmittalButton" name="junkGeneratorSubmittalButton" type="submit" value="Submit">Generate Again</button>
		</div>
</div>
<?php
	}
	else
	{
?>
<div id="introductionTextContainer" class="innerContainer">
	This is my Junk Generator. Use it to generate massive amounts of junk in millions of bytes.
</div>
<div class="hrDiv"></div>
<div id="junkGeneratorFormContainer" class="junkGeneratorFormContainer">
	<form id="junkGeneratorForm" name="junkGeneratorForm" action="/?page=junkgenerator" method="post">
		<div>Please enter the number of bytes you would like to generate. Discover something interesting about this form. :-> (limit is <?php echo PHP_INT_MAX; ?>.)</div>
		<div class="hrSeparator"></div>
		<div>
			<label for="numberOfBytes">Number of Bytes&nbsp;</label>
			<input id="numberOfBytes" name="numberOfBytes" type="text" value=""/>
		</div>
		<div class="formItemSeparator"></div>
		<div>
			<button id="junkGeneratorSubmittalButton" name="junkGeneratorSubmittalButton" type="submit" value="Submit">Generate</button>
		</div>
	</form>
</div>
<?php
	}
?>