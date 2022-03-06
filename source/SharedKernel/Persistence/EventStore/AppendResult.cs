﻿namespace SharedKernel.Persistence.EventStore;

public class AppendResult
{
  public AppendResult(long nextExpectedVersion)
  {
    NextExpectedVersion = nextExpectedVersion;
  }

  public long NextExpectedVersion { get; }
}