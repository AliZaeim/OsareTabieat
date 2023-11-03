/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */



CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.extraPlugins = 'dialog,dialogui,clipboard,widget,codesnippet';
    config.language = 'fa';
    config.contentsLangDirection = 'rtl';
    config.uiColor = '#a1d3f2';

    config.filebrowserImageUploadUrl = "/UsersPanel/Commons/UploadFile";
    config.filebrowserUploadUrl = "/UsersPanel/Commons/UploadFile";
    config.filebrowserBrowseUrl = "/UsersPanel/Commons/FileBrowser";
    config.allowedContent = true;
    config.extraPlugins = 'html5video,widget,widgetselection,clipboard,lineutils';
    config.line_height = ".5cm;1cm;1.5cm;2cm;2.5cm;3cm;3.5cm";
    config.extraPlugins = 'lineheight,wordcount';
    config.wordcount = {        
        showCharCount: true,
        countHTML: true,
        countSpacesAsChars: false,
        hardLimit: true,
        warnOnLimitOnly: true,
    };
    

};

