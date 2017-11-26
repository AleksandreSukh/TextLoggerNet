# TextLoggerNet

Main Features
* It is thread safe, you can use it without worrying of threading exceptions 
* It automatically deletes file if it grows larger than 20 mb
* Can be customised by implementing building-block interfaces like ItextloggerTextFormatter (to change line formatting for example)
* It formats TimeSpan in a human readable format like "4 days and 4 minutes (since something)" instead of 4:00:04:00 (since something)
* Contains very handy tools like RunningMethodLogger which can be used to log in a specifig interval which task is being executed and how much time is spent since started.
* And the best for the last it is working in production environment for some years without any issues
