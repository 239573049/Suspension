@echo off
setlocal

REM 设置TARGET_DIR为当前bat文件的目录下的bin子目录
set TARGET_DIR=%~dp0bin

REM 递归删除 libSkiaSharp.dll 和 SkiaSharp.dll 文件
for /R "%TARGET_DIR%" %%f in (libSkiaSharp.dll 1e7b63404cd2fb8e6525b2fd4ee4d286.png) do (
    if exist "%%f" (
        del "%%f"
        echo Deleted: %%f
    )
)

REM 递归删除所有 .pdb 文件
for /R "%TARGET_DIR%" %%f in (*.pdb) do (
    if exist "%%f" (
        del "%%f"
        echo Deleted: %%f
    )
)

REM 递归删除所有 .js.map 文件
for /R "%TARGET_DIR%" %%f in (*.js.map) do (
    if exist "%%f" (
        del "%%f"
        echo Deleted: %%f
    )
)

endlocal
