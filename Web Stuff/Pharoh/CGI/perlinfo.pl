#!/usr/bin/perl -w

for $key (sort keys %ENV)
{
	print "$key = $ENV{$key}\n";
}

