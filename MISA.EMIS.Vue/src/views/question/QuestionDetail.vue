<template>
    <!-- Dialog -->
    <div class="m-dialog">
        <div class="m-dialog-modal"></div>

            <div 
                @click="handleShowSolutionContent"
                :class="{
                    'm-dialog-question-left': !isShowSolutionContent,
                    'm-dialog-back-to-question': isShowSolutionContent
                }"
            >
                <div 
                    class="m-dialog-question-icon"
                    :class="{
                        'icon-question-left': !isShowSolutionContent,
                        'icon-back-to-question': isShowSolutionContent
                    }"
                >
                </div>

                <div class="m-dialog-question-text">
                    {{ isShowSolutionContent ? 'Quay lại câu hỏi' : 'Xem giao diện làm bài' }}
                </div>
            </div>
            <div @click="handleShowSolutionContent" v-show="!isShowSolutionContent" class="m-dialog-question-right">
                <div class="m-dialog-question-icon icon-question-right"></div>

                <div class="m-dialog-question-text">
                    Thêm lời giải
                </div>
            </div>

            <BaseSolution 
                @handleShowSolutionContent="handleShowSolutionContent"
                v-model="question.SolutionContent"
            />

        <div 
            :class="{ 
                'm-dialog-box-fill-blank': question.QuestionType === myEnum.questionType.fillBlank, 
                'm-dialog-box-essay': question.QuestionType === myEnum.questionType.essay,
                'm-dialog-box-select-answer': question.QuestionType === myEnum.questionType.selectAnswer
            }"
            class="m-dialog-box"
            v-show="!isShowSolutionContent"
        >
            

            <!-- Question -->
            <div class="m-dialog-question">
                <div class="m-dialog-question-header">
                    <BaseComboBox
                        :options="questionTypeOptions"
                        v-model="question.QuestionType"
                        placeholder=''
                        :tabindex="5"
                        :widthValue="296"
                        :index="questionIndexOption"
                    />
                </div>

                <VueEditor 
                    :editorToolbar="customToolbar"
                    v-model="question.QuestionContent"
                    placeholder="Nhập câu hỏi tại đây..."
                >
                </VueEditor>

                <div class="m-dialog-question-add-file">
                    <div v-for="(questionFile, index) in question.QuestionFiles" :key="index" class="m-dialog-question-add-file-item">
                        <div @click="deleteQuestionFileOnClick(questionFile)" class="m-dialog-question-add-file-icon icon-x"></div>

                        <img :src="`${baseFileUrl}${questionFile}`" @error="onErrorQuestionFile($event, questionFile)">
                    </div>

                    <div @click="btnQuestionFileOnClick" class="m-dialog-question-add-file-item">
                        <div class="m-dialog-question-add-file-btn icon-plus"></div>
                        <input 
                            ref="QuestionFileInput" 
                            v-on:change="handleFileUpload"
                            class="m-exercise-select-file-hide" type="file"
                        />
                    </div>
                </div>
                
            </div>

            <!-- Answer -->
            <div>
                <SelectAnswerQuestion 
                    v-show="question.QuestionType === myEnum.questionType.selectAnswer"
                    v-model="question.Answers"
                    ref="selectAnswer"
                />

                <TrueFalseQuestion 
                    v-show="question.QuestionType === myEnum.questionType.trueFalse"
                    v-model="question.Answers"
                    ref="trueFalse"
                />

                <FillBlankQuestion 
                    v-show="question.QuestionType === myEnum.questionType.fillBlank"
                    v-model="question.Answers"
                    ref="fillBlank"
                />
            </div>

            <!-- Footer -->
            <div class="m-dialog-footer">
                <div class="m-dialog-footer-left">
                    <button @click="btnAddAnswerOnClick" class="m-btn m-second-btn">
                        Thêm đáp án
                    </button>
                </div>

                <div class="m-dialog-footer-right">
                    <button @click="btnCloseOnClick" class="m-btn m-second-btn">
                        Hủy
                    </button>
                    <button @click="btnSaveOnClick" class="m-btn m-btn-default">
                        Lưu
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import BaseComboBox from '../../components/base/BaseComboBox.vue'
import { VueEditor } from "vue2-editor";
import SelectAnswerQuestion from './question-type/SelectAnswerQuestion.vue'
import TrueFalseQuestion from './question-type/TrueFalseQuestion.vue'
import FillBlankQuestion from './question-type/FillBlankQuestion.vue'
import myEnum from '../../utils/enum.js'
import myConstant from '../../utils/constant.js'
import { mapGetters } from 'vuex'
import BaseSolution from '../../components/base/BaseSolution.vue'
import request from "../../utils/request";
import env from '../../utils/env';
import Toastify from "../../utils/toastify";

export default {
    name: 'question-detail',

    components: {
        BaseComboBox,
        VueEditor,
        SelectAnswerQuestion,
        TrueFalseQuestion,
        FillBlankQuestion,
        BaseSolution,
    },

    data() {
        return {
            question: {
                QuestionType: myEnum.questionType.selectAnswer,
                QuestionContent: "",
                QuestionFiles: [],
                Answers: [],
                SolutionContent: '',
            },
            
            myEnum: myEnum,

            customToolbar: myConstant.defaultSettingToolbar,

            isShowSolutionContent: false,

            questionTypeOptions: [
                { value: myEnum.questionType.selectAnswer, text: 'Chọn đáp án đúng' },
                { value: myEnum.questionType.trueFalse, text: 'Đúng sai' },
                { value: myEnum.questionType.fillBlank, text: 'Điền vào chỗ trống' },
                { value: myEnum.questionType.essay, text: 'Tự luận' }
            ],

            baseFileUrl: env.baseFileUrl,
        }
    },

    props: {
        formMode: {
            type: Number,
            default: myEnum.formMode.view,
        },

        questionForEdit: {
            type: Object,
            default: null,
        },

        questionIndex: {
            type: Number,
            default: 0
        },

        questionType: {
            type: Number,
            default: 0
        }
    },

    computed: {
        ...mapGetters([
            'exercise',
        ]),

        questionIndexOption() {
            let index = 1;

            if (this.questionIndex !== null && this.questionIndex !== undefined) {
                index = this.questionIndex + 1;
            }
            else {
                index = this.exercise && this.exercise.Questions && this.exercise.Questions.length ? this.exercise.Questions.length + 1 : 1;
            }

            return index;
        },
    },

    methods: {
        /**
         * Sự kiện khi click vào button close
         * Author: LMDuc (11/12/2021)
         */
        btnCloseOnClick() {
            this.$emit('closeQuestionDetail')

            // Reset answer image
            this.$refs.selectAnswer.$data.answerImgUrls = []
        },

        /**
         * Method để validate input
         * Author: LMDuc (23/12/2021)
         */
        validateInput() {
            let question = this.question;

            // Validate nội dung câu hỏi
            if (!(question.QuestionContent || (question.QuestionFiles && question.QuestionFiles.length))) {
                return 'Câu hỏi phải có nội dung hoặc file đính kèm';
            }

            // Validate các đáp án
            if (question.QuestionType === myEnum.questionType.essay) {
                return '';
            }

            if (!(question.Answers && question.Answers.length)) {
                return 'Đáp án không được để trống';
            }

            // Đáp án câu hỏi chọn đáp án đúng
            if (question.QuestionType === myEnum.questionType.selectAnswer) {
                let correctAnswerCount = 0;

                for (const answer of question.Answers) {
                    if (!(answer.AnswerContent || answer.AnswerImg)) {
                        return 'Đáp án phải có nội dung hoặc file đính kèm';
                    }

                    if (answer.CorrectAnswer) {
                        correctAnswerCount++;
                    }
                }

                if (correctAnswerCount === 0) {
                    return 'Câu hỏi ít nhất phải có 1 đáp án đúng';
                }
            }
            
            // Đáp án câu hỏi đúng sai
            if (question.QuestionType === myEnum.questionType.trueFalse) {
                let correctAnswerCount = 0;

                for (const answer of question.Answers) {
                    if (!answer.AnswerContent) {
                        return 'Đáp án phải có nội dung';
                    }

                    if (answer.CorrectAnswer) {
                        correctAnswerCount++;
                    }
                }

                if (correctAnswerCount === 0) {
                    return 'Câu hỏi ít nhất phải có 1 đáp án đúng';
                }
            }

            // Đáp án câu hỏi điền vào chỗ trống
            if (question.QuestionType === myEnum.questionType.fillBlank) {
                for (const answer of question.Answers) {
                    if (!answer.AnswerContent) {
                        return 'Đáp án phải có nội dung';
                    }
                }
            }

        },
        

        /**
         * Sự kiện khi click vào button Thêm đáp án
         * Author: LMDuc (11/12/2021)
         */
        btnAddAnswerOnClick() {
            if (this.question.QuestionType !== myEnum.questionType.essay) {
                this.question.Answers.push({ AnswerContent: '', AnswerImg: '', CorrectAnswer: false })
            }
        },

        /**
         * Sự kiện khi click vào button save
         * Author: LMDuc (11/12/2021)
         */
        btnSaveOnClick() {
            let errorMessage = this.validateInput();

            if (errorMessage) {
                // Hiển thị cảnh báo cho ngươi dùng biết
                Toastify.showPopupMsg({
                    yesCallback: () => {},
                    title: "",
                    iShowBtnNo: false,
                    btnSaveText: 'Đóng',
                    msg: errorMessage,
                });

                return;
            }

            let question = {...this.question};

            if (this.formMode === myEnum.formMode.add || this.formMode === myEnum.formMode.clone) {
                this.$store.dispatch('exercise/addExerciseQuestion', question);
            }
            else if (this.formMode === myEnum.formMode.edit) {
                this.$store.dispatch('exercise/updateExerciseQuestion', { questionIndex: this.questionIndex, question});
            }

            this.btnCloseOnClick();
        },

        /**
         * Function xử lý ẩn hiện solution content
         * Author: LMDuc (20/12/2021)
         */
        handleShowSolutionContent() {
            this.isShowSolutionContent = !this.isShowSolutionContent;
        },

        /**
         * Xử lý sự kiện khi click vào nút chọn ảnh
         * Author: LMDuc (13/12/2021)
         */
        btnQuestionFileOnClick() {
            this.$refs.QuestionFileInput.click();
        },

        /**
         * Xử lý upload image
         * Author: LMDuc (22/12/2021)
         */
        handleFileUpload() {
            let questionFile = this.$refs.QuestionFileInput.files[0];

            // Initialize the form data
            let formData = new FormData();
            formData.append('file', questionFile);

            request({
                method: "post",
                url: "/Exercises/UploadFile",
                data: formData,
                headers: {
                    'Content-Type': 'multipart/form-data'
                },
            }, false)
                .then((res) => {
                    this.question.QuestionFiles.push(res.Data);
                })
                .catch((error) => {
                console.log(error);
                });
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
                e.target.src = require('../../assets/icon/ic_excel.jpg')
            } else if (fileExtension === 'doc' || fileExtension === 'docx') {
                e.target.src = require('../../assets/icon/ic_word.jpg')
            } else if (fileExtension === 'pptx' || fileExtension === 'ppt') {
                e.target.src = require('../../assets/icon/ic_pptx.jpg')
            } else if (fileExtension === 'pdf') {
                e.target.src = require('../../assets/icon/ic_pdf.png')
            } else if (fileExtension === 'mp4') {
                e.target.src = require('../../assets/icon/ic_Video_24.svg')
            } else if (fileExtension === 'mp3') {
                e.target.src = require('../../assets/icon/ic_audio-file.png')
            }
            else {
                e.target.src = require('../../assets/icon/attach.jpg')
            }
        },

        /**
         * Xử lý sự kiện khi click vào icon xóa question file
         * Author: LMDuc (22/12/2021)
         */
        deleteQuestionFileOnClick(questionFile) {
            this.question.QuestionFiles = this.question.QuestionFiles.filter(file => file !== questionFile);
        },

        changeAnswers(val) {
            switch (val) {
                case 0:
                    this.question.Answers = [
                        { AnswerContent: '', AnswerImg: '', CorrectAnswer: false },
                        { AnswerContent: '', AnswerImg: '', CorrectAnswer: false },
                        { AnswerContent: '', AnswerImg: '', CorrectAnswer: false },
                        { AnswerContent: '', AnswerImg: '', CorrectAnswer: false }
                    ]
                    break;

                case 1:
                    this.question.Answers = [
                        { AnswerContent: 'Đúng', AnswerImg: '', CorrectAnswer: false },
                        { AnswerContent: 'Sai', AnswerImg: '', CorrectAnswer: false }
                    ]
                    break;

                case 2:
                    this.question.Answers = [
                        { AnswerContent: '', AnswerImg: '', CorrectAnswer: false }
                    ]
                    break;

                case 3:
                    this.question.Answers = []
                    break;
                
                default:
                    break;
            }
        }
    },

    watch: {
        formMode: function (formModeValue) {
            // Clean error message
            // me.validateError = {
            //     TeacherCode: "",
            //     FullName: "",
            //     Email: "",
            //     LeavingDate: "",
            // };

            if (formModeValue === myEnum.formMode.add) {
                // Clean form khi mở form để add
                this.question = {
                    ...this.question,
                    QuestionType: this.questionType,
                    QuestionContent: "",
                    QuestionFiles: [],
                    SolutionContent: '',
                    Answers: [],
                }

                this.changeAnswers(this.questionType);
            } 
            else if (formModeValue === myEnum.formMode.edit || formModeValue === myEnum.formMode.clone) {
                if (!this.questionForEdit) {
                    return;
                }

                let question = JSON.parse(JSON.stringify(this.questionForEdit));
                let answers = [...question.Answers]
                this.question = question;
                console.log('object', question.Answers)
                
                this.$nextTick(() => {
                    this.question.Answers = answers;
                })
                
                // // Focus vào ô nhập liệu đầu tiên
                // me.$refs.FullName.focus();
                
            }
        },

        'question.QuestionType': {
            immediate: true,

            handler(val) {
                this.changeAnswers(val);
            }
        }
    }
}
</script>

<style scoped>
    /* Dialod box */
    .m-dialog .m-dialog-box {
        min-height: 600px;
    }

    .m-dialog-question-header::v-deep div.m-combobox-value {
        font-size: 16px;
        font-weight: 700;
        color: #4e5b6a;
        width: 297px;
        height: 40px;
        background-color: #f8e373;
        border-radius: 10px 0 10px 0;
        line-height: 40px;
        padding-left: 12px;
        box-sizing: border-box;
        border: none;
    }

    .m-dialog-question-header::v-deep .m-combobox-button {
        background-color: #f8e373;
    }

    /* Editor */
    .m-dialog-question::v-deep .ql-snow {
        border: none !important;
    }

    .m-dialog-question::v-deep .ql-toolbar.ql-snow {
        position: absolute;
        z-index: 10;
        right: 0;
        top: 0;
    }

    .m-dialog-question::v-deep .ql-editor {
        margin-top: 46px;
        height: calc(316px - 116px);
        margin-bottom: 70px;
    }

    .m-dialog-box-fill-blank .m-dialog-question::v-deep .ql-editor {
        height: calc(240px - 116px) !important;
        min-height: 116px;
        margin-bottom: 70px;
    }

    .m-dialog-box-essay .m-dialog-question::v-deep .ql-editor {
        height: calc(600px - 116px) !important;
        margin-bottom: 70px;
    }
</style>