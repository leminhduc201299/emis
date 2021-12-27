<template>
  <div 
    v-on:keyup.space="open = !open"
    v-on:keyup.down="onKeyDownUp"
    v-on:keyup.up="onKeyUpUp"
    v-on:keyup.enter="onItemClick(options[target])"
    :tabindex="tabindex" 
    @focusout="handleFocusout" 
    class="m-combobox" :style="{ width: widthValue + 'px' }"
  >
    <div 
      class="m-combobox-value" 
      :class="{ 'm-input-invalid': isInvalid }"
    >
      {{ selected ? ( index ? `Câu ${index} - ${selected.text}` : selected.text)  : placeholder }}
    </div>
    <!-- <input  :value="selected ? selected.text : ''" type="text" class="m-combobox-input" :placeholder="placeholder"> -->
    <button @click="open = !open" class="m-combobox-button icon-dropdown"></button>

    <div 
      v-show="open" 
      :class="{ 'm-combobox-select-top': isSelectTop }"
      class="m-combobox-select"
    >
        <div 
          v-for="(option, index) of options"
          :key="index"
          :class="{'m-item-selected': option.value === (selected ? selected.value : ''), 'm-combobox-target': option.value == (options ? options[target].value : '')}"
          @click="onItemClick(option)"
          class="m-checkbox m-combobox-item"
        >
            <div class="checkbox-text">{{ option.text }}</div>
        </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "combo-box",

  props: {
    options: {
      type: Array,
      required: true,
    },
    value: {
      type: [Number, String, Object],
    },
    defaultValue: {
      type: Object,
      default: null
    },
    placeholder: {
      type: String,
      default: ''
    },
    widthValue: {
      type: Number,
      default: 218,
    },
    tabindex: {
      type: Number,
      required: false,
      default: 0,
    },
    isSelectTop: {
      type: Boolean,
      default: false
    },
    isInvalid: {
    },
    index: {
      type: Number,
      default: 0
    }
  },

  data() {
    return {
      selected: this.defaultValue ? this.defaultValue: { text: '', value: '' },
      target: 0,
      open: false,
    };
  },

  methods: {
    /**
     * Method thực hiện chọn 1 item
     * Author: LMDuc (21/11/2021)
     */
    onItemClick(option) {
      this.selected = option;
      this.open = false;
      this.$emit('input', option.value);
      this.$emit('change');
    },

    /**
     * Mehtod thực hiện khi focus out ra khỏi combobox
     * Author: LMDuc (21/11/2021)
     */
    handleFocusout(e) {
      // if the blur was because of outside focus
      // currentTarget is the parent element, relatedTarget is the clicked element
      if (!e.currentTarget.contains(e.relatedTarget)) {
          this.open = false;
      }
    },

    /**
     * Method xử lý sự kiện khi nhấn vào key down
     * Author: LMDuc (21/11/2021)
     */
    onKeyDownUp() {
      if (this.target == this.options.length - 1) {
        this.target = 0
      } else {
        this.target++;
      }
    },

    /**
     * Method xử lý sự kiện khi nhấn vào key up
     * Author: LMDuc (21/11/2021)
     */
    onKeyUpUp() {
      if (this.target == 0) {
        this.target = this.options.length - 1
      } else {
        this.target--;
      }
    }
  },

  computed: {
    changeData() {
      const { value, options } = this
      return {
        value,
        options
      }
    }
  },

  watch: {
    changeData: {
      immediate: true,

      handler(changeDataVal) {
        let value = changeDataVal.value;
        let options = changeDataVal.options;

        let optionSelected = null;
        if (value !== null && value !== undefined && options  && options.length > 0) {
          const optionArr = options.filter(option => option.value === value);
          optionSelected = optionArr[0] ? optionArr[0] : { text: '', value: '' };
        }

        this.selected = optionSelected;

        console.log("value: ", value);
      }
    },

  },
};
</script>

<style scoped>
</style>