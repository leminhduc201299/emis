<template>
    <div class="m-dialog-answer">
        <div 
            v-for="(answer, index) in answers" :key="index"  
            class="m-dialog-answer-item"
            :style="{ 'background-color': myConstant.answerBackgroundColor[index % 4] }"
        >
            <div class="m-answer-item-header">
                <div class="m-answer-item-header-left">{{ String.fromCharCode(index + 65) }}</div>
                <div class="m-answer-item-header-right">
                    <div @click="btnMoreOnClick(index)" class="m-answer-item-header-icon icon-more-2">
                        <div 
                            v-show="answerListFocus && answerListFocus.length ? answerListFocus.includes(index) : false" 
                            class="m-more m-cursor-pointer"
                        >
                            <div class="m-more-item">
                                <div @click="btnAnswerImgOnClick(index)" class="m-more-text">Thêm ảnh<i class="fa fa-hand-rock-o" aria-hidden="true"></i></div>
                            </div>

                            <div class="m-more-item">
                                <div @click="btnDeleteAnswerOnClick(index)" class="m-more-text">Xóa đáp án</div>
                            </div>

                        </div>

                        <input 
                            :ref="`AnswerImg${index}`" 
                            v-on:change="handleFileUpload(index)"
                            class="m-exercise-select-file-hide" type="file" accept="image/*"
                        />

                    </div>

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

                <img v-if="answerImgUrls[index] || answer.AnswerImg" :src="answerImgUrls[index] ? answerImgUrls[index] : `${baseFileUrl}${answer.AnswerImg}`">
            </div>
            
        </div>
    </div>
</template>

<script>
import BaseEditable from '../../../components/base/BaseEditable.vue'
import myConstant from '../../../utils/constant.js'
import request from "../../../utils/request";
import env from '../../../utils/env'
import Toastify from "../../../utils/toastify";

export default {
    name: 'select-answer-question',

    components: {
        BaseEditable,
    },

    data() {
        return {
            answers: [],

            answerListFocus: [],

            answerImgUrls : [],

            myConstant: myConstant,
            baseFileUrl: env.baseFileUrl,
        }
    },

    methods: {
        /**
         * Xử lý sự kiện khi click vào icon more
         * Author: LMDuc (13/12/2021)
         */
        btnMoreOnClick(index) {
            if (this.answerListFocus.includes(index)) {
                this.answerListFocus = this.answerListFocus.filter(itemSelectd => itemSelectd !== index)
            } else {
                this.answerListFocus.push(index);
            }
        },

        /**
         * Xử lý sự kiện khi click vào btn delete
         * Author: LMDuc (13/12/2021)
         */
        btnDeleteAnswerOnClick(index) {
            if (index !== null && index !== undefined) {
                this.answers = this.answers.filter((item, idx) => idx !== index);
                this.answerImgUrls = this.answerImgUrls.filter((item, idx) => idx !== index);
                console.log('object')
            }

            this.$emit('input', this.answers);
        },

        /**
         * Xử lý sự kiện khi click vào btn check
         * Author: LMDuc (13/12/2021)
         */
        btnCheckAnswerOnClick(index) {
            this.$set(this.answers[index], 'CorrectAnswer', !this.answers[index].CorrectAnswer);
            // this.answers[index].CorrectAnswer = !this.answers[index].CorrectAnswer;

            this.$emit('input', this.answers);
        },

        /**
         * Xử lý sự kiện khi nội dung đáp án thay đổi
         * Author: LMDuc (13/12/2021)
         */
        handleAnswerContentChange(index, value) {
            this.answers[index].AnswerContent = value;

            this.$emit('input', this.answers);
        },

        /**
         * Xử lý sự kiện khi click vào nút chọn ảnh
         * Author: LMDuc (13/12/2021)
         */
        btnAnswerImgOnClick(index) {
            this.$refs[`AnswerImg${index}`][0].click();
            this.btnMoreOnClick(index);
        },

        /**
         * Xử lý upload image
         * Author: LMDuc (13/12/2021)
         */
        handleFileUpload(index) {
            let answerFile = this.$refs[`AnswerImg${index}`][0].files[0];
            let fileExtension = answerFile.name.split('.').pop().toLowerCase();

            if (!myConstant.imageFileExtension.includes(fileExtension)) {
                // Hiển thị cảnh báo cho ngươi dùng biết
                Toastify.showPopupMsg({
                    yesCallback: () => {},
                    title: "",
                    iShowBtnNo: false,
                    btnSaveText: 'Đóng',
                    msg: 'Định dạng file của đáp án không đúng',
                });

                return;
            }

            this.$set(this.answerImgUrls, index, URL.createObjectURL(answerFile));

            // Initialize the form data
            let formData = new FormData();
            formData.append('file', answerFile);

            request({
                method: "post",
                url: "/Exercises/UploadFile",
                data: formData,
                headers: {
                    'Content-Type': 'multipart/form-data'
                },
            }, false)
                .then((res) => {
                    this.answers[index].AnswerImg = res.Data;
                    this.$emit('input', this.answers);
                })
                .catch((error) => {
                console.log(error);
                });
        },
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