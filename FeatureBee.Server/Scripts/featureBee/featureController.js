var FeatureBeeController = function (boardHub, editPanelHub, request, conditionController) {
    this.add = {
        post: function(feature) {
            boardHub.server.addNewItem(
                   {
                       name: feature.name,
                       team: feature.team,
                       description: feature.description,
                       link: feature.link
                   });
        }
    };

    this.edit = {
        post: function (feature) {
            feature.conditions = conditionController.trimEmptyConditions(feature.conditions);
            editPanelHub.server.editItem(
            {
                name: feature.name,
                description: feature.description,
                link: feature.link,
                conditions: feature.conditions
            });
        }
    };

    this.find = {
        all: function () {
            var features = request.get('/api/features');
            $.each(features, function (index, feature) {
                if (feature) {
                    feature.conditions = conditionController.addMissingConditions(feature.conditions);
                }
            });

            return features;
        },
        byId: function (featureId) {
            var feature = request.get('/api/features/?id=' + featureId);
            if (feature) {
                feature.conditions = conditionController.addMissingConditions(feature.conditions);
            }

            return feature;
        }
    };
};