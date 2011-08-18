package net.phyer.systems.messaging.teamchat;

public interface Server {

  void setStatus(final ServerStatus status);

  ServerStatus getStatus();

}
