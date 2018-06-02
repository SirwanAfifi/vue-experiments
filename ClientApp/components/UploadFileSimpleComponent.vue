<template>
    <div class="container">
        <h3>Add a new Coffee</h3>
        <form>
            <div class="form-group">
                <label class="control-label">Coffee Name</label>
                <input required type="text" class="form-control" name="Name">
            </div>

            <div class="form-group">
                <label class="control-label">Coffee Type</label>
                <select class="form-control" name="CoffeeType">
                    <option v-for="coffeeType in coffeeTypes" :value="coffeeType.value">{{ coffeeType.name }}</option>
                </select>
            </div>

            <div class="form-group">
                <label class="control-label">Coffee Image</label>
                <input required type="file" multiple class="form-control" name="Image">
            </div>

            <button class="btn btn-primary" type="submit">Save</button>
        </form>
    </div>
</template>

<script>
export default {
        data() {
            return {
                coffeeTypes: [
                    { name: 'Espresso', value: 0, },
                    { name: 'Latte', value: 1, },
                    { name: 'Mocha', value: 2, },
                ]
            };
        }
}
</script>
