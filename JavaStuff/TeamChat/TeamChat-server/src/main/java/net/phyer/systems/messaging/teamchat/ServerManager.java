package net.phyer.systems.messaging.teamchat;

public interface ServerManager {

  void start();

  void stop();

  void restart();

  ServerStatus status();

}
