import unittest
from list_operations import ListOperations


class TestListOperations(unittest.TestCase):

    def setUp(self):
        self.list_ops = ListOperations()

    def test_add_number_and_get_average(self):
        self.list_ops.add_number(10)
        self.list_ops.add_number(20)
        self.list_ops.add_number(30)
        self.assertEqual(self.list_ops.get_average(), 20)

    # This test is designed to fail
    def test_fail_average(self):
        self.list_ops.add_number(10)
        self.list_ops.add_number(20)
        self.list_ops.add_number(30)
        self.assertEqual(self.list_ops.get_average(), 25)  # Incorrect expected value


if __name__ == '__main__':
    unittest.main()