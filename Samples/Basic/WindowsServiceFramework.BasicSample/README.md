// Creating event log entries. Make sure run this command as an Administrator

eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO WindowsServiceFramework.BasicSample.Service /D "Test"
eventcreate /ID 1 /L APPLICATION /T WARNING /SO WindowsServiceFramework.BasicSample.Service /D "Test"
eventcreate /ID 1 /L APPLICATION /T ERROR  /SO WindowsServiceFramework.BasicSample.Service /D "Test"

