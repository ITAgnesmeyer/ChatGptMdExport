# ChatGptMdExport

A small tool to convert the json files from the ChatGPT export into individual MarkDown files (currently conversations.json).

Each chat is converted into its own MarkDown file.

```cmd
paramters:
/?              Help
/i:<folder>     folder containing ChatGPT export
/o:<folder>     folder to export to
/cd             create destination folder

example:
ChatGptMdExport /i:"c:\temp\gpt_export" /o:"c:\temp\md_folder"
```