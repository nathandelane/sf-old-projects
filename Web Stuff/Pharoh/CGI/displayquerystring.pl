#!/usr/bin/perl -w

if (length ($ENV{'QUERY_STRING'}) > 0)
{
	$buffer = $ENV{'QUERY_STRING'};
	@pairs = split (/&/, $buffer);
	foreach $pair (@pairs)
	{
		($name, $value) = split (/=/, $pair);
		print "$name = $value\n";
	}
}

