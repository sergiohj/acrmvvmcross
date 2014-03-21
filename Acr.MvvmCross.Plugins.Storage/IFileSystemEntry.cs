﻿using System;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IFileSystemEntry {

        string Name { get; }
        string FullName { get; }
        bool Exists { get; }

        DateTime LastWriteTime { get; }
        DateTime LastAccessTime { get; }

        //void Copy(string destination);
        void Delete();
        void Move(string destination);
        void Rename(string newName);
    }
}