<template>
    <div class="m-solution-box">
        <div class="m-solution-body">
            <VueEditor 
                :editorToolbar="customToolbar"
                v-model="content"
                placeholder="Nhập lời giải tại đây..."
            >
            </VueEditor>

            <div class="m-solution-footer">
                <button @click="closeSolutionContent" class="m-btn m-second-btn">
                    Hủy
                </button>

                <button @click="btnSaveOnClick" class="m-btn m-btn-default">
                    Lưu
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import { VueEditor } from "vue2-editor";
import myConstant from '../../utils/constant.js'

export default {
    name: 'base-solution',

    components: {
        VueEditor,
    },

    props: {
        value: {
            type: String,
            default: ''
        }
    },

    data() {
        return {
            content: '',

            customToolbar: myConstant.defaultSettingToolbar,
        }
    },

    methods: {
        /**
         * Function xử lý ẩn solution content
         * Author: LMDuc (20/12/2021)
         */
        closeSolutionContent() {
            this.content = this.value;
            this.$emit('handleShowSolutionContent');
        },

        /**
         * Function xử save solution content
         * Author: LMDuc (20/12/2021)
         */
        btnSaveOnClick() {
            this.$emit('input', this.content);
            this.$emit('handleShowSolutionContent');
        }
    },

    watch: {
        value: function(val) {
            if (val !== null && val !== undefined) {
                this.content = val;
            }
        }
    }    
}
</script>

<style scoped>
    /* Editor */
    .m-solution-body::v-deep .ql-snow {
        border: none !important;
    }

    .m-solution-body::v-deep .ql-toolbar.ql-snow {
        position: absolute !important;
        z-index: 10;
        right: 0;
        top: 0;
    }

    .m-solution-body::v-deep .ql-editor {
        margin-top: 46px;
        height: calc(600px - 115px);
    }
</style>