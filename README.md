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

PUBLISH AOT is active!

### Convert ot HTML
```cmd
/i:source            folder
/o:destination       folder
/c                   create destination folder if not exist
/h                   help
mdtohtml /i:"F:\ChatGPT\test_out" /o:"F:\ChatGPT\test_html" /c
```

# MdToHtml Functionality

MdToHtml is a conversion tool specifically designed to convert Markdown files into HTML files. Here is a more detailed description of its functionality:

1. **Input**: The tool takes a Markdown file as input. Markdown is a lightweight markup language often used for writing documentation and content for the web. It uses a simple text formatting syntax that can be converted into HTML.

2. **Processing**: MdToHtml reads the Markdown file and parses the syntax contained within. It recognizes various Markdown elements such as headers, lists, links, images, code blocks, and more.

3. **Conversion**: After the tool has parsed the Markdown syntax, it begins converting these elements into their corresponding HTML equivalents. For example, a header in Markdown, denoted with `#`, is converted into an `<h1>` tag in HTML.

4. **Output**: After all the Markdown elements have been converted into HTML, the tool generates a new HTML file. This file has the same content as the original Markdown file, but is now in HTML format, which can be displayed in web browsers.

5. **Display**: The user can open the generated HTML file and view it in a web browser. The HTML file retains the formatting and structure of the original Markdown file, so the content is displayed exactly as it was written in Markdown.

MdToHtml is a useful tool for anyone who regularly works with Markdown and needs a simple way to convert these files into HTML.

