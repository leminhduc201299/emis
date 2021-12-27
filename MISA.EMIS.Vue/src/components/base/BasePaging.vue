<template lang="">
    <!-- Phần phân trang -->
    <div class="navigation-left">
        <div @click="firstPage" :class="{'disable': page <= 1}" class="navigation-change icon-first"></div>
        <div @click="backPage" :class="{'disable': page <= 1}" class="navigation-change icon-back"></div>

        <input ref="pageInput" v-on:keyup.enter="onEnterPageNumber" :value="page" class="m-input navigation-page" style="width: 64px;" type="number">

        <div @click="nextPage" :class="{'disable': page >= totalPage}" class="navigation-change icon-next"></div>
        <div @click="lastPage" :class="{'disable': page >= totalPage}" class="navigation-change icon-last"></div>

        <div class="navigation-text"><b>{{ page }}/{{ totalPage }}</b> trang (<b>{{ totalRecord }}</b> bản ghi)</div>
    </div>
</template>

<script>
export default {
    name: 'paging',

    data() {
        return {
            page: this.value
        }
    },

    props: {
        value: {
            type: Number,
            default: 0,
        },
        totalPage: {
            type: Number,
            default: 0,
        },
        totalRecord: {
            type: Number,
            default: 0,
        }
    },
    
    methods: {
        /**
         * Method xử lý sự kiện nhảy tới trang đầu tiên
         * Author: LMDuc (18/11/2021)
         */
        firstPage() {
            let currentPage = this.page;

            if (currentPage > 1) {
                this.page = 1;
                this.$emit('input', 1);
            }
        },

        /**
         * Method xử lý sự kiện quay lại trang trước đó
         */
        backPage() {
            let currentPage = this.page;

            if (currentPage > 1) {
                this.page = currentPage - 1;
                this.$emit('input', currentPage - 1);
            }
        },

        /**
         * Method xử lý sự kiện nhảy tới trang tiếp theo
         * Author: LMDuc (18/11/2021)
         */
        nextPage() {
            let currentPage = this.page;

            if (currentPage < this.totalPage) {
                this.page = currentPage + 1;
                this.$emit('input', currentPage + 1);
            }
        },

        /**
         * Method xử lý sự kiện nhảy tới trang cuối cùng
         * Author: LMDuc (18/11/2021)
         */
        lastPage() {
            let currentPage = this.page;

            if (currentPage < this.totalPage) {
                this.page = this.totalPage;
                this.$emit('input', this.totalPage);
            }
        },

        /**
         * Method xử lý sự kiện khi người dùng nhập số trang
         * Author: LMDuc (18/11/2021)
         */
        onEnterPageNumber(event) {
            let value = event.target.value;
            value = parseInt(value);

            let pageNumber = value;

            if (value < 1) {
                pageNumber = 1;
            }

            if (value > this.totalPage) {
                pageNumber = this.totalPage;
            }

            if (pageNumber != this.page) {
                this.page = pageNumber;
                this.$emit('input', pageNumber);
            } else {
                this.$refs.pageInput.value = pageNumber;
            }
            
        },
    },

    watch: {
        value: function(value) {
            this.page = value;
        }
    }
};
</script>

<style scoped>
</style>