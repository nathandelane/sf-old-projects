# How to get going in Linux - Debian flavor

sudo apt-get update
sudo apt-get install ruby ruby-dev
sudo gem install watir
ruby
require 'rubygems'
require 'watir'
require 'watir-webdriver'
browser = Watir::Browser.new
browser.goto 'http://www.overstock.com/'
