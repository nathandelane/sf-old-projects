package com.nathandelane.education;

public final class Result {

    private final long totalTimeInMilliseconds;
    private final PassedOrFailed passedOrFailed;

    public Result(final long totalTime, final PassedOrFailed passedOrFailed) {
	this.totalTimeInMilliseconds = totalTime;
	this.passedOrFailed = passedOrFailed;
    }

    public long getTotalTime() {
	return this.totalTimeInMilliseconds;
    }

    public PassedOrFailed getPassedOrFailed() {
	return this.passedOrFailed;
    }

}
