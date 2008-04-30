function init()
{
	if(readCookie("lang") == null)
	{
		createCookie("lang", "en", 30);
	}
	else
	{
		selectLanguage(readCookie("lang"));
	}
}

function createCookie(name,value,days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name) {
	createCookie(name,"",-1);
}

function activateMenu(menuName)
{
	if(menuName == "language-text")
	{
		document.getElementById("languageTextMenu").className = "languageTextMenu visible";
	}
	else if(menuName == "choose-your-art")
	{
		if(document.getElementById("chooseYourArtMenuContainer").className == "chooseYourArtMenuContainer hidden")
		{
			document.getElementById("chooseYourArtMenuContainer").className = "chooseYourArtMenuContainer visible";
		}
		else if(document.getElementById("chooseYourArtMenuContainer").className == "chooseYourArtMenuContainer visible")
		{
			document.getElementById("chooseYourArtMenuContainer").className = "chooseYourArtMenuContainer hidden";
		}
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
	
	createCookie("lang", language, 30);
	document.getElementById("languageTextMenu").className = "languageTextMenu hidden";
	
	return;
}
