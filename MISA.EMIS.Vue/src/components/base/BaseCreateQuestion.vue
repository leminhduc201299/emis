<template>
    <div class="create-question-content">
        <div
            @click="showQuestionDetailOnClick(
                myEnum.formMode.add,
                null,
                null,
                myEnum.questionType.selectAnswer
            )"
            class="create-question-item"
        >
            <div class="create-question-icon icon-select-answer"></div>
            <div class="create-question-text">{{ mode === myEnum.questionListMode.empty ? 'Chọn đáp án': 'Thêm câu chọn đáp án' }}</div>
        </div>

        <div
            @click="showQuestionDetailOnClick(
                myEnum.formMode.add,
                null,
                null,
                myEnum.questionType.trueFalse
            )"
            class="create-question-item"
        >
            <div class="create-question-icon icon-true-false"></div>
            <div class="create-question-text">{{ mode === myEnum.questionListMode.empty ? 'Đúng sai': 'Thêm câu đúng sai' }}</div>
        </div>

        <div
            @click="showQuestionDetailOnClick(
                myEnum.formMode.add,
                null,
                null,
                myEnum.questionType.fillBlank
            )"
            class="create-question-item"
        >
            <div class="create-question-icon icon-fill-blank"></div>
            <div class="create-question-text">{{ mode === myEnum.questionListMode.empty ? 'Điền vào chỗ trống': 'Thêm câu điền vào chỗ trống' }}</div>
        </div>

        <div
            @click="showQuestionDetailOnClick(
                myEnum.formMode.add,
                null,
                null,
                myEnum.questionType.essay
            )"
            class="create-question-item"
        >
            <div class="create-question-icon icon-essay"></div>
            <div class="create-question-text">{{ mode === myEnum.questionListMode.empty ? 'Tự luận': 'Thêm câu tự luận' }}</div>
        </div>
    </div>
</template>

<script>
import myEnum from "../../utils/enum";

export default {
    name: 'base-create-question',

    data() {
        return {
            myEnum: myEnum,
        }
    },

    methods: {
        /**
         * Method mở dialog question detail theo form mode
         * Author: LMDuc (11/12/2021)
         */
        showQuestionDetailOnClick(formMode, question, questionIndex, questionType) {
            this.$emit("showQuestionDetail", formMode, question, questionIndex, questionType);
        },
    },

    props: {
        mode: {
            type: Number,
            default: myEnum.questionListMode.empty
        }
    }
}
</script>

<style scoped>
    
</style>