document.addEventListener("DOMContentLoaded",function(){document.querySelectorAll("[data-choices]").forEach(function(e){var t="input"===e.tagName.toLowerCase();new Choices(e,{removeItemButton:"true"===e.dataset.choicesRemoveitembutton,itemSelectText:"",maxItemCount:5,searchEnabled:!1,placeholder:!0,placeholderValue:e.getAttribute("placeholder")||"Select an option",classNames:{containerInner:t?"form-control":"form-select"}})})}),document.addEventListener("DOMContentLoaded",function(){document.querySelectorAll("[data-choices-innertext]").forEach(function(o){new Choices(o,{removeItemButton:"true"===o.dataset.choicesRemoveitembutton,itemSelectText:"",searchEnabled:!1,placeholder:!0,classNames:{containerInner:o.dataset.choicesClassname||"form-select"},callbackOnCreateTemplates:function(c){return{item:function(e,t){var a=o.querySelector(`option[value="${t.value}"]`).dataset.color;return c(`
             <div class="${e.item} ${t.highlighted?e.highlightedState:e.itemSelectable}"
                  data-item data-id="${t.id}" data-value="${t.value}" data-deletable>
               <span class="rounded-circle ${a} p-1 me-2 icon-shape " style="width:8px; height:8px"></span>
               ${t.label}
             </div>
           `)},choice:function(e,t){var a=o.querySelector(`option[value="${t.value}"]`).dataset.color;return c(`
             <div class="${e.item} ${e.itemChoice} ${t.disabled?e.itemDisabled:e.itemSelectable}"
                  data-select-text="${this.config.itemSelectText}" data-choice data-id="${t.id}" data-value="${t.value}" data-choice-selectable>
               <span class="rounded-circle ${a} p-1 me-2 icon-shape " style="width:8px; height:8px"></span>
               ${t.label}
             </div>
           `)}}}})})});