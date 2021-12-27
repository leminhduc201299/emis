class Validate {
    /**
     * Thực hiện validate email trong form
     * Author: LMDuc (21/10/2021)
     */
    static validateEmail(value) {
        const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (re.test(String(value).toLowerCase())) {
            return true;
        }

        return false;
    }
}

export default Validate;