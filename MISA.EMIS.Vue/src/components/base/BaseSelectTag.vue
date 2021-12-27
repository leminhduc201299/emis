<template>
  <div class="m-select-tag">
      <div class="search-tag">
        <div 
            v-for="(option, index) in options" :key="index" 
            class="search-tag-item"
            @click="btnTagItemOnClick(option)"
            :class="{
                'search-tag-item-selected': selectedTags.includes(option)
            }"
        >
            {{ option }}
        </div>
    </div>

    <div class="m-dialog-textarea">
        <div 
            v-for="(selectedTag, index) in selectedTags" :key="index" 
            class="search-tag-item"
            @click="btnTagItemOnClick(selectedTag)"
        >
            {{ selectedTag }}
        </div>
    </div>
    
  </div>
</template>

<script>
export default {
    name: 'base-select-tag',

    data() {
        return {
            selectedTags: []
        }
    },

    props: {
        options: {
            type: Array,
            required: true,
        },

        value: {
            type: Array,
            default: () => [],
        },

        placeholder: {
            type: String,
            default: ''
        }
    },

    methods: {
        btnTagItemOnClick(item) {
            if (this.selectedTags.includes(item)) {
                this.selectedTags = this.selectedTags.filter(itemSelectd => itemSelectd !== item)
            } else {
                this.selectedTags.push(item);
            }

            this.$emit('input', this.selectedTags);
        },
    },

    watch: {
        value: {
            immediate: true,

            handler(val) {
                if (val && val.length) {
                    this.selectedTags = val;
                }
                else {
                    this.selectedTags = [];
                }
            }
        },
    }
};
</script>