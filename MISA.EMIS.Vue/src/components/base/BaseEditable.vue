<template>
  <textarea
    class="m-editable" 
    :placeholder="placeholder"
    ref="editable"
    contenteditable
    @input="onInput"
  ></textarea>
</template>

<script>
export default {
    name: 'base-editable',

    props: {
        index: {
            type: Number,
            default: 0
        },
        value: {
            type: String,
            default: '',
        },

        placeholder: {
            type: String,
            default: ''
        }
    },

    mounted() {
        this.$refs.editable.value = this.value;
    },

    methods: {
        onInput(e) {
            this.$emit('input', this.index, e.target.value);

            let inputEl = e.target;

            if (inputEl.scrollHeight <= 180) {
                inputEl.style.height = 'auto';
                inputEl.style.height = inputEl.scrollHeight + 'px';
            }
            else {
                inputEl.style.overflowY = 'overlay';
            }

            if (!e.target.value) {
                inputEl.style.height = '59px';
            }
        },
    },

    watch: {
        value: {
            immediate: true,

            handler(val) {
                if (!this.$refs.editable) {
                    return;
                }
                
                this.$refs.editable.value = val;

                if (this.$refs.editable.scrollHeight <= 180) {
                    this.$refs.editable.style.height = 'auto';
                    this.$refs.editable.style.height = this.$refs.editable.scrollHeight + 'px';
                }
                else {
                    this.$refs.editable.style.height = '180px';
                    this.$refs.editable.style.overflowY = 'overlay';
                }
            }
        }
    } 
};
</script>