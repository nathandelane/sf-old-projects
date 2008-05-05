<?php
    /**
     * index.php - website entry point.
     * Description: index.php is my front page. This page will contain references
     *  to other pages on my site.
     * @author Nathan Lane <nathamberlane@gmail.com>
     * @copyright Copyright (c) 2008, Nathan Lane
     */
    
    $page_content = "pages/index.php";
    $page_dtd = "lib/doctype/xhtml_transitional.php";
    $page_title = "Home | Nathandelane: programmer, designer, husband, father";
    
    if(file_exists("_templates/default_template.php")) {
        require_once("_templates/default_template.php");
    } else {
        echo "File Not Found Error. Could not find default_template.php";
    }
?>