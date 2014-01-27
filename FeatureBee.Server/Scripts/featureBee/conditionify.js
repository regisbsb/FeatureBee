$(function () {
    /*
    wrapper to attach events to the condition template, as the template
    is rendered by handlebars after page initialization
    */

    $.widget("as24.conditionify", {
        options: {
            getTemplate: function (element) {
                return Handlebars.compile($(element).find('[data-container="condition-item"]').html().trim());
            },

            conditions: [],
            triggerAdd: 'data-add-condition-value',
            triggerDelete: 'data-delete',
            triggerNew: 'data-add-condition',

            add: function () { },
            delete: function () { },
            new: function () { }
        },

        baseTemplate: null,
        templates: [],

        apply: function (name) {

        },

        render: function (name, type, data) {
            var self = this;

            $("body").on("click", '[' + self.options.triggerAdd + '="' + type + '"]', function () { self._renderItem(this, name, type, data); });

            $(data).each(function (index, value) {
                $("body").off("click", '[' + self.options.triggerDelete + '="{ "' + type + '" : "' + value + '" }"]');
                $("body").on("click", '[' + self.options.triggerDelete + '="{ "' + type + '" : "' + value + '" }"]', function () {
                    var data = JSON.parse($(this).attr(self.options.triggerDelete));
                    self.options.delete({ name: name, type: type, values: [data[type]] });
                    $(this).parent().remove();
                });
            });

            self.renderAddNewCondition(name);
        },

        renderAddNewCondition: function (name) {
        },

        _renderItem: function (ele, name, type, data) {
            var self = this;
            var template = self.templates[type];
            if (template) {
                var element = $(self.templates[type](data));
                element.attr('data-adding', 'true');
                $(ele).siblings('[data-adding]').remove();
                $(ele).after(element);
                $(element).find('[data-action="add"]').on('click', function () {
                    var datastate = {
                        name: name,
                        type: type,
                        values: []
                    };
                    $(element).find('[data-value]').each(function (index, value) {
                        datastate.values.push($(this).val());
                    });

                    self.options.add(datastate);
                    data.push(datastate.values[0]);
                    $(element).parent().html(self.baseTemplate({ name: name, type: type, values: data }));
                    $(element).remove();
                });
            }
        },

        _create: function () {
            var self = this;
            self.baseTemplate = self.options.getTemplate(self.element);
            for (var i = 0; i < this.options.conditions.length; i++) {
                var condition = this.options.conditions[i];
                var source = $(condition.template).html().trim();
                var template = Handlebars.compile(source);
                this.templates[condition.type] = template;
            }
        },

        _destroy: function () {
            $("body").off("click", '[' + self.options.triggerDelete + ']');
            $("body").off("click", '[' + self.options.triggerAdd + ']');
        }
    });
});