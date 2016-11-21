var app = function() {
    var config = {
        resourcesUrl: "http://localhost:58190/",
        cssPath: "Css/",
        jsPath: "Scripts/",
        imagePath: "Images/",
        fontPath: "fonts/"
    };
    return {
        SiteConfig: {
            ResourcesUrl: config.resourcesUrl,
            CssPath: config.cssPath,
            CssFullPath: config.resourcesUrl + config.cssPath,
            JsPath: config.jsPath,
            JsFullPath: config.resourcesUrl + config.jsPath,
            ImagePath: config.imagePath,
            ImageFullPath: config.resourcesUrl + config.imagePath,
            FontPath: config.fontPath,
            FontFullePath: config.resourcesUrl + config.fontPath
        }
    }
}();