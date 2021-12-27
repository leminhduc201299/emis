const getters = {
    exercises: state => state.exercise.exercises,
    exercise: state => state.exercise.exercise,
    totalRecord: state => state.exercise.totalRecord,

    grades: state => state.grade.grades,
    subjects: state => state.subject.subjects,
    topics: state => state.topic.topics,
    searchTags: state => state.searchTag.searchTags,
}

export default getters