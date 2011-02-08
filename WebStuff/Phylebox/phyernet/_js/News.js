/**
 * News.js
 * 
 * This script defines the News JavaScript functions, including the news rotator.
 */
if (Phyer && $Phyer) {
	var News = function() { }
	
	News.prototype = {
		/**
		 * Constants
		 */
		DEFAULT_ROTATION_INTERVAL: 3.5,
		/**
		 * Static fields
		 */
		__rotationTimer: null,
		__numNewsItems: 4,
		
		/**
		 * showNewsItem
		 * Shows a specific news item.
		 * @param int itemIndex
		 * @return void
		 */
		showNewsItem: function(itemIndex) {
			$("div[id*=newsItem][class=newsItem]").fadeOut();
			$("div[id*=newsItem][class=newsItem]").attr("class", "hide newsItem");
			$("div[id=newsItem" + itemIndex + "]").fadeIn();
			$("div[id=newsItem" + itemIndex + "]").attr("class", "newsItem");
		},
		
		/**
		 * Starts the banner rotation based on the current item index.
		 * @param int timeoutInSeconds
		 * @return void
		 */
		_startRotation: function(timeoutInSeconds) {
			if (typeof timeoutInSeconds == "undefined" || timeoutInSeconds == 0) {
				timeoutInSeconds = $Phyer.News.DEFAULT_ROTATION_INTERVAL;
			}
			
			var timeoutInMillseconds = timeoutInSeconds * 1000;
			
			$Phyer.News.__rotationTimer = setTimeout("$Phyer.News._rotateNextNewsItem();", timeoutInMillseconds);
		},
		
		/**
		 * Rotates to the next news item.
		 * @return void
		 */
		_rotateNextNewsItem: function() {
			var currentNewsItem = $("div[id*=newsItem][class=newsItem]");
			var index = parseInt(currentNewsItem.attr("id").substring("newsItem".length));
			
			if (index < ($Phyer.News.__numNewsItems - 1)) {
				index++;
			} else {
				index = 0;
			}

			$Phyer.News.showNewsItem(index);
			
			$Phyer.News._startRotation($Phyer.News.DEFAULT_ROTATION_INTERVAL);
		}
	}
	
	Phyer.prototype.News = new News();
}

window.$Phyer = new Phyer();