package net.phyer.systems.messaging.teamchat;

public enum ServerStatus {

  STARTING("Starting"),
  RUNNING("Running"),
  RESTARTING("Restarting"),
  STOPPING("Stopping"),
  STOPPED("Stopped");

  private String status;

  ServerStatus(final String status) {
    this.status = status;
  }

  public String getStatus() {
    return this.status;
  }

  public static ServerStatus fromValue(final String value) {
    return Enum.valueOf(ServerStatus.class, value);
  }

}
