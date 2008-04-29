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
		
	}
	
	document.getElementById("languageTextMenu").className = "languageTextMenu hidden";
	
	return;
}
