var ConditionController = function (allConditions) {
    var inArray = function (needle, haystack, key) {
        var i;
        for (i = 0; i < haystack.length; i++) {
            if (haystack[i][key] === needle[key]) {
                return true;
            }
        }

        return false;
    };

    this.addMissingConditions = function (conditions) {
        $.each(allConditions, function (index, value) {
            if (!inArray(value, conditions, 'type')) {
                conditions.push(value);
            }
        });

        return conditions;
    };

    this.trimEmptyConditions = function (conditions) {
        var result = [];
        $.each(conditions, function (index, value) {
            if (value.values)
                result.push(value);
        });

        return result;
    };
};