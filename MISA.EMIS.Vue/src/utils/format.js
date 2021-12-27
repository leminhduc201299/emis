class Format {
    /**
     * 
     * @param {Date} dateOfBirth Ngày cần format
     * @param {String} type yyyy/mm/dd or dd/mm/yy...
     * @returns dateString
     */
    static formatDate(dateOfBirth, type) {
        let dateString = '';
        if (dateOfBirth) {
            dateOfBirth = new Date(dateOfBirth);
            let date = dateOfBirth.getDate();
            let month = dateOfBirth.getMonth() + 1;
            let year = dateOfBirth.getFullYear();
            month = month < 10 ? `0${month}` : month;
            date = date < 10 ? `0${date}` : date;

            switch (type) {
                case 'dd/mm/yyyy':
                    dateString = `${date}/${month}/${year}`;
                    break;

                case 'yyyy-mm-dd':
                    dateString = `${year}-${month}-${date}`;
                    break;

                default:
                    break;
            }
        }
        return dateString;
    }

}

export default Format;