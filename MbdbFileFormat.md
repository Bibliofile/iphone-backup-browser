# iTunes backup file format #

As [this article](http://code.google.com/p/iphonebackupbrowser/wiki/MbdbMbdxFormat) points out, iTunes backups consist of an index file and a data file.
In iTunes 10.5 backups, index file .mbdx seems to be missing.
Therefore, the mbdb file has to be parsed record-by-record.
The main problem is to find the filename of the record in the backup folder. It turned out that the backup's filename is always created using this algorithm:
_SHA1( RecordDomain + "-" + RecordFileNameOnDevice )_
This makes it possible to parse every iTunes backup without the need of having a corresponding mbdx file.