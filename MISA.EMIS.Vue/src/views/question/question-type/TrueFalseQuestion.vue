<template>
    <div class="m-dialog-answer">
        <div 
            v-for="(answer, index) in answers" :key="index"  
            class="m-dialog-answer-item"
            :style="{ 'background-color': myConstant.answerBackgroundColor[index % 4] }"
        >
            <div class="m-answer-item-header">
                <div class="m-answer-item-header-right">
                    <div 
                        @click="btnCheckAnswerOnClick(index)"
                        class="m-answer-item-header-icon icon-uncheck"
                        :class="{ 'icon-checked': answer.CorrectAnswer }"
                    >
                    </div>
                </div>
            </div>

            <div class="m-answer-item-content">
                <BaseEditable 
                    placeholder="Nhập đáp án..."
                    :index="index"
                    :value="answers[index].AnswerContent"
                    @input="handleAnswerContentChange"
                />
            </div>
            
        </div>
    </div>
</template>

<script>
import BaseEditable from '../../../components/base/BaseEditable.vue'
import myConstant from '../../../utils/constant.js'

export default {
    name: 'true-false-question',

    components: {
        BaseEditable,
    },

    data() {
        return {
            answers: [],

            myConstant: myConstant,
        }
    },

    methods: {
        /**
         * Xử lý sự kiện khi click vào btn check
         * Author: LMDuc (20/12/2021)
         */
        btnCheckAnswerOnClick(index) {
            this.answers = this.answers.map(answer => ({ AnswerContent: answer.AnswerContent, AnswerImg: answer.AnswerImg, CorrectAnswer: false }))
            this.$set(this.answers[index], 'CorrectAnswer', true);

            this.$emit('input', this.answers);
        },

        /**
         * Xử lý sự kiện khi nội dung đáp án thay đổi
         * Author: LMDuc (13/12/2021)
         */
        handleAnswerContentChange(index, value) {
            this.answers[index].AnswerContent = value;

            this.$emit('input', this.answers);
        }
    },

    props: {
        value: {
            type: Array,
            default: () => [],
        },
    },

    watch: {
        value: {
            immediate: true,

            handler(val) {
                if (val && val.length) {
                    this.answers = val;
                }
                else {
                    this.answers = [];
                }
            }
        },
    }
}
</script>

<style scoped>
    
</style>