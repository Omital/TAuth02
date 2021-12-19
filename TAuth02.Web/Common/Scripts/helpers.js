var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('TAuth02');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);