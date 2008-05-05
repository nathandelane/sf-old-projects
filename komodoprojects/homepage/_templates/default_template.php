<?php
    /**
     * default_template.php - this is the default template for this site.
     * @author Nathan Lane <nathamberlane@gmail.com>
     * @copyright Copyright (c) 2008, Nathan Lane
     */
    
    //
    if(file_exists("lib/config/config.php")) {
        require("lib/config/config.php");
    
        if(isset($page_dtd)) {
            if(file_exists($web_root . $page_dtd)) {
                require_once($web_root . $page_dtd);
            } else {
                echo "Could not find ". $web_root . $page_dtd;
            }
        }
    } else {
        echo "Application configuration file could not be found.";
    }
?>
<html>
    <head>
        <title><?php if(isset($page_title)) { echo $page_title; } else { echo "Untitled Page | Nathandelane"; } ?></title>
        <link type="text/css" href="_stylesheets/main.css" rel="stylesheet"/>
    </head>
    <body>
        <?php
            require_once($page_content);
        ?>
    </body>
</html>
