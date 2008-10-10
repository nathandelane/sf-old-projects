<?php
session_start();
?>
<div id="innerPageBody" class="innerPageBody">
	<h1>Error</h1>
	<p>
		I am sorry, however an error occurred due to the request you made. Please return to the <a href="<?php echo $sitePath; ?>">home page</a> or use the navigation above to find the page you are looking for.
	</p>
	<h2>
		<?php
		if(isset($_SESSION['errorNumber']) && isset($_SESSION['errorMessage']))
			{
		?>
		HTTP <?php echo $_SESSION['errorNumber']; ?> Error: <?php echo $_SESSION['errorMessage']; ?>
		<?php
			}
			else
			{
		?>
		Unknown HTTP Error Occurred!
		<?php
			}
		?>
	</h2>
</div>
