@ECHO OFF
ROBOCOPY /MIR "%1esec\esec" "%2esec" >nul
IF NOT ERRORLEVEL 8 ROBOCOPY /MIR "%1esec\esdlc" "%2esdlc" >nul
IF NOT ERRORLEVEL 8 ROBOCOPY /MIR "%1pylib" "%2lib" >nul

rem ERRORLEVEL==1 is not actually an error, it means that files were updated.
rem Only errors 8 and above are serious enough to require a clean/rebuild.
IF ERRORLEVEL 8 (
    RMDIR /s/q "%2esec" > nul
    RMDIR /s/q "%2esdlc" > nul
    RMDIR /s/q "%2lib" > nul
    ECHO Copying Python source files failed. The destination folders have been cleaned, and a rebuild is required.
    rem Reset the error code to break the build.
    CMD /C EXIT 1
) ELSE (
    rem Reset nonzero error codes.
    CMD /C EXIT 0
)
