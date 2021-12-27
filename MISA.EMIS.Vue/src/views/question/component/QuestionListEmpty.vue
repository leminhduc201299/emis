<template>
    <div class="question-content-empty">
        <div class="import-title">
            Nhập khẩu / Tách câu hỏi tự động sử dụng công nghệ AI
        </div>
        <div class="import-content">
            <div class="import-content-icon icon-drop_file"></div>
            <div class="import-content-text">
            Nhấn để tải lên file bài tập hoặc kéo thả file vào đây. File có định dạng xls, xlsx, doc, docx, pdf
            </div>
            <button class="m-btn m-btn-purple import-content-btn">
                Tải tệp Excel mẫu
            </button>
        </div>

        <div class="create-question-title">hoặc tự tạo câu hỏi mới</div>
        <BaseCreateQuestion 
            :mode="myEnum.questionListMode.empty"
            @showQuestionDetail="showQuestionDetail"
        />

        <!-- Dialog QuestionDetail -->
        <QuestionDetail
            v-show="isShowQuestionDetail"
            :formMode="formMode"
            :questionIndex="questionIndex"
            :questionType="questionType"
            :questionForEdit="questionForEdit"
            @closeQuestionDetail="closeQuestionDetail"
        />

    </div>
</template>

<script>
import QuestionDetail from "../QuestionDetail.vue";
import myEnum from "../../../utils/enum.js";
import BaseCreateQuestion from "../../../components/base/BaseCreateQuestion.vue";

export default {
    name: 'question-list-empty',

    components: {
        QuestionDetail,
        BaseCreateQuestion,
    },

    data() {
        return {
            isShowQuestionDetail: false,
            formMode: myEnum.formMode.view,
            questionIndex: null,
            questionType: null,
            questionForEdit: null,

            myEnum: myEnum,
        }
    },

    methods: {
        /**
         * Method mở dialog question detail theo form mode
         * Author: LMDuc (11/12/2021)
         */
        showQuestionDetail(formMode, question, questionIndex, questionType) {
            this.formMode = formMode;
            this.questionForEdit = question;
            this.questionIndex = questionIndex;
            this.questionType = questionType;

            this.isShowQuestionDetail = true;
        },

        /**
         * Method để đóng dialog thông tin question
         * Author: LMDuc (11/12/2021)
         */
        closeQuestionDetail() {
            this.formMode = myEnum.formMode.view;
            this.isShowQuestionDetail = false;
        },
    },
}
</script>

<style scoped>
    
</style>