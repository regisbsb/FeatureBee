var FeatureBeeController = function (boardHub, editPanelHub, request) {
    this.add = {
        get: function() {},
        post: function(feature) {
            boardHub.server.addNewItem(
                   {
                       name: feature.name,
                       team: feature.team,
                       description: feature.description,
                       link: feature.link,
                       conditions: feature.conditions
                   });
        }
    };

    this.edit = {
        get: function (featureId) {
            var feature = request.get('/api/features/?id=' + featureId);
            return feature;
        },
        post: function(feature) {
            editPanelHub.server.editItem(
            {
                name: feature.name,
                description: feature.description,
                link: feature.link
            });
        }
    };
};