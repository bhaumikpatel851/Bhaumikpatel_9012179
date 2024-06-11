class ListOperations:
    def _init_(self):
        self.numbers = []

    def add_number(self, number):
        if not isinstance(number, (int, float)):
            raise ValueError("Only numbers are allowed")
        self.numbers.append(number)

    def get_average(self):
        if not self.numbers:
            raise ValueError("Cannot calculate average of an empty list")
        return sum(self.numbers) / len(self.numbers)