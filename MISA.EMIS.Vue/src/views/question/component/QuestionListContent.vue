<template>
    <div class="question-content-list">
        <div class="tool-title">
            <div class="tool-title-left">
                <button style="margin-left: 0;" class="m-btn m-btn-purple">
                    Chọn câu từ thư viện học liệu
                </button>

                <button class="m-btn m-btn-purple">
                    Sắp xếp thứ tự câu hỏi
                </button>
            </div>

            <div class="tool-title-right">
                <button class="m-btn m-btn-blue">
                    Nhập khẩu
                </button>

                <button class="m-btn m-btn-blue">
                    Tách câu hỏi tự động sử dụng AI
                </button>
            </div>
        </div>

        <div class="question-list">
            
            <div 
                v-for="(question, index) in exercise.Questions" :key="index" 
                class="question-item"
                :class="{
                    'question-item-select-answer': question.QuestionType == myEnum.questionType.selectAnswer,
                    'question-item-true-false': question.QuestionType == myEnum.questionType.trueFalse,
                    'question-item-fill-blank': question.QuestionType == myEnum.questionType.fillBlank,
                    'question-item-essay': question.QuestionType == myEnum.questionType.essay
                }"
            >
                <div class="question-body">
                    <div class="question-body-text">
                        <b>{{ index + 1 }}.</b> <span v-html="question.QuestionContent"></span>
                    </div>

                    <div v-if="question.QuestionFiles && question.QuestionFiles.length" class="question-body-file">
                        <div v-for="(questionFile, index) in question.QuestionFiles" :key="index" class="question-body-file-item">
                            <img :src="`${baseFileUrl}${questionFile}`" @error="onErrorQuestionFile($event, questionFile)">
                        </div>
                    </div>

                    <div v-if="question.Answers && question.Answers.length" class="question-body-answer">
                        <div v-for="(answer, answerIndex) in question.Answers" :key="answerIndex" class="question-body-answer-item">
                            <div 
                                class="answer-item-name"
                                :class="{ 'answer-item-name-active': answer.CorrectAnswer || question.QuestionType === myEnum.questionType.fillBlank }"
                            >
                                {{ question.QuestionType === myEnum.questionType.fillBlank ? answerIndex + 1 : String.fromCharCode(answerIndex + 65) }}
                            </div>

                            <div v-if="!answer.AnswerImg" class="answer-item-content">
                                {{ answer.AnswerContent | formatFillBlankAnswer(question.QuestionType) }}
                            </div>

                            <img v-else :src="`${baseFileUrl}${answer.AnswerImg}`" class="answer-item-img">
                        </div>
                    </div>

                    <div v-if="question.SolutionContent" class="question-body-solution">
                        <b>{{ 'Lời giải' }}:</b> <span v-html="question.SolutionContent"></span>
                    </div>
                </div>

                <div class="question-footer">
                    <div class="question-footer-right">
                        <button @click="showQuestionDetail(myEnum.formMode.edit, question, index, question.QuestionType)" class="m-btn m-second-btn">
                            Chỉnh sửa
                        </button>

                        <button @click="showQuestionDetail(myEnum.formMode.clone, question, index, question.QuestionType)" class="m-btn m-btn-action icon-duplicate">
                        </button>

                        <button @click="deleteQuestionOnClick(index)" class="m-btn m-btn-action icon-delete">
                        </button>
                    </div>
                </div>
            </div>
            
        </div>

        <BaseCreateQuestion 
            :mode="myEnum.questionListMode.content"
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
import { mapGetters } from 'vuex'
import QuestionDetail from "../QuestionDetail.vue";
import myEnum from "../../../utils/enum.js";
import BaseCreateQuestion from "../../../components/base/BaseCreateQuestion.vue";
import Toastify from "../../../utils/toastify";
import env from '../../../utils/env'
import myConstant from '../../../utils/constant.js'

export default {
    name: 'question-list-content',

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
            baseFileUrl: env.baseFileUrl,
        }
    },

    methods: {
        /**
         * Method mở dialog question detail theo form mode
         * Author: LMDuc (20/12/2021)
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
         * Author: LMDuc (20/12/2021)
         */
        closeQuestionDetail() {
            this.formMode = myEnum.formMode.view;
            this.isShowQuestionDetail = false;
        },

        /**
         * Method để đóng delete question
         * Author: LMDuc (20/12/2021)
         */
        deleteQuestionOnClick(index) {
            // Hiển thị cảnh báo cho ngươi dùng biết
            Toastify.showPopupMsg({
                yesCallback: () => this.deleteQuestionHandle(index),
                title: "",
                iShowBtnNo: true,
                msg: 'Bạn có chắc chắn muốn xóa câu hỏi này không?',
            });
        },

        /**
         * Method để đóng delete question
         * Author: LMDuc (20/12/2021)
         */
        deleteQuestionHandle(index) {
            this.$store.dispatch('exercise/deleteExerciseQuestion', index);
        },

        /**
         * Xử lý sự kiện khi load question file bị lỗi
         * Author: LMDuc (22/12/2021)
         */
        onErrorQuestionFile(e, questionFile) {
            let fileExtension = questionFile.split('.').pop().toLowerCase();

            e.target.onerror = null; 
            if (myConstant.imageFileExtension.includes(fileExtension)) {
                e.target.src = `${env.baseFileUrlTemp}${questionFile}`
            } else if (fileExtension === 'xlsx' || fileExtension === 'xls' || fileExtension === 'csv') {
                e.target.src = require('../../../assets/icon/ic_excel.jpg')
            } else if (fileExtension === 'doc' || fileExtension === 'docx') {
                e.target.src = require('../../../assets/icon/ic_word.jpg')
            } else if (fileExtension === 'pptx' || fileExtension === 'ppt') {
                e.target.src = require('../../../assets/icon/ic_pptx.jpg')
            } else if (fileExtension === 'pdf') {
                e.target.src = require('../../../assets/icon/ic_pdf.png')
            } else if (fileExtension === 'mp4') {
                e.target.src = require('../../../assets/icon/ic_Video_24.svg')
            } else if (fileExtension === 'mp3') {
                e.target.src = require('../../../assets/icon/ic_audio-file.png')
            }
            else {
                e.target.src = require('../../../assets/icon/attach.jpg')
            }
        },
    },

    computed: {
        ...mapGetters([
            'exercise',
        ]),
    },

    filters: {
        formatFillBlankAnswer: function(answerContent, questionType) {
            if (questionType !== myEnum.questionType.fillBlank) {
                return answerContent;
            }
            
            let contentRes = answerContent.replaceAll(',', '; ');

            return contentRes;
        }
    }
}
</script>

<style scoped>
    
</style>