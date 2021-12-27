<template>
    <div class="m-dialog-answer">

        <div v-for="(answer, index) in answers" :key="index" class="m-dialog-blank-item">
            <div class="m-dialog-blank-text">{{`Ô trống ${index + 1}`}}</div>
            <div class="m-dialog-blank-body">
                <div class="m-dialog-blank-selected-list">
                    <div v-for="(item, itemIndex) in answer.AnswerContent" :key="itemIndex" class="m-dialog-blank-selected-item">{{ item }}
                        <div @click="onItemDeleteClick(index, itemIndex)" class="m-dialog-blank-selected-item-icon icon-x"></div>
                    </div>
                </div>

                <input v-on:keyup.enter="onBlankEnter(index)" :ref="`blank${index}`" class="m-input" type="text" placeholder="">

                 <div @click="btnDeleteAnswerOnClick(index)" class="m-dialog-blank-body-icon icon-x"></div>
            </div>
        </div>

    </div>
</template>

<script>
export default {
    name: 'fill-blank-question',

    data() {
        return {
            answers: [],
        }
    },

    methods: {
        /**
         * Xử lý sự kiện khi click vào btn delete
         * Author: LMDuc (20/12/2021)
         */
        btnDeleteAnswerOnClick(index) {
            if (index !== null && index !== undefined) {
                this.answers = this.answers.filter((item, idx) => idx !== index);
            }

            let answers = this.answers.map(item => ({ AnswerContent: item.AnswerContent.join(','), AnswerImg: item.AnswerImg, CorrectAnswer: item.CorrectAnswer }))
            this.$emit('input', answers);
        },

        /**
         * Xử lý sự kiện khi nhấn phím enter ở ô trống
         * Author: LMDuc (20/12/2021)
         */
        onBlankEnter(index) {
            console.log('object', this.$refs[`blank${index}`][0].value)
            let value = this.$refs[`blank${index}`][0].value;
            
            if (value) {
                this.answers[index].AnswerContent.push(this.$refs[`blank${index}`][0].value);
                this.$refs[`blank${index}`][0].value = '';

                let answers = this.answers.map(item => ({ AnswerContent: item.AnswerContent.join(','), AnswerImg: item.AnswerImg, CorrectAnswer: item.CorrectAnswer }))
                this.$emit('input', answers);
            }
        },

        /**
         * Xử lý sự kiện khi nhấn nút x ở item
         * Author: LMDuc (20/12/2021)
         */
        onItemDeleteClick(index, itemIndex) {
            this.answers[index].AnswerContent = this.answers[index].AnswerContent.filter((item, itemIdx) => itemIdx !== itemIndex);

            let answers = this.answers.map(item => ({ AnswerContent: item.AnswerContent.join(','), AnswerImg: item.AnswerImg, CorrectAnswer: item.CorrectAnswer }))
            this.$emit('input', answers);
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
                    this.answers = val.map(item => {
                        let answerContentList = item.AnswerContent ? item.AnswerContent.split(',') : [];

                        return {
                            AnswerContent: answerContentList, 
                            AnswerImg: item.AnswerImg, 
                            CorrectAnswer: item.CorrectAnswer,
                        }
                    })
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