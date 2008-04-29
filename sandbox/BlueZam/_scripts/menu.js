function activateMenu(menuName)
{
	if(menuName == "language-text")
	{
		document.getElementById("languageTextMenu").className = "languageTextMenu visible";
	}
	
	return;
}

function selectLanguage(language)
{
	if(language == "en")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px 0px";
	}
	else if(language == "sp")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -10px";
	}
	else if(language == "de")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -20px";
	}
	else if(language == "da")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -30px";
	}
	else if(language == "nl")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -40px";
	}
	else if(language == "fr")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -50px";
	}
	else if(language == "hu")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -60px";
	}
	else if(language == "ja")
	{
		document.getElementById("languageSetting").style.backgroundPosition = "0px -70px";
	}
	
	document.getElementById("languageTextMenu").className = "languageTextMenu hidden";
	
	return;
}
