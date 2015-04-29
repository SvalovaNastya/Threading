cd Threading/bin/Debug

start cmd /c "Threading.exe 20000 server"
start cmd /c "Threading.exe 20001 threadserver"
start cmd /c "Threading.exe 20002 taskserver"