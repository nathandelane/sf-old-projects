package com.nathandelane.csvquery.file;

import java.util.Map;

import com.google.common.collect.Maps;

public final class CsvFileRepository {

  private static CsvFileRepository INSTANCE;

  private final Map<String, CsvFile> files;

  private CsvFileRepository() {
    files = Maps.newHashMap();
  }

  public void addFile(String name, CsvFile csvFile) {
    files.put(name, csvFile);
  }

  public void removeFile(String name) {
    files.remove(name);
  }

  public CsvFile getFile(String name) {
    return files.get(name);
  }

  public boolean fileIsLoaded(String name) {
    return files.containsKey(name);
  }

  public String[] fileNames() {
    return files.keySet().toArray(new String[] {});
  }

  public static CsvFileRepository getInstance() {
    if (INSTANCE == null) {
      synchronized (CsvFileRepository.class) {
        if (INSTANCE == null) {
          INSTANCE = new CsvFileRepository();
        }
      }
    }

    return INSTANCE;
  }

}
