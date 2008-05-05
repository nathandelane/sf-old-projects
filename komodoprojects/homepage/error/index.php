<?php
    /**
     * index.php - default error page.
     * @author Nathan Lane <nathamberlane@gmail.com>
     * @copyright Copyright (c) 2008, Nathan Lane
     */
    
    $page_content = "error.php";
    $page_dtd = "../lib/doctype/xhtml_transitional.php";
    $page_title = "Error | Nathandelane: an error occurred";
    
    if(file_exists("../_templates/error_template.php")) {
        require_once("../_templates/error_template.php");
    } else {
        echo "File Not Found Error. Could not find error_template.php";
    }
?>