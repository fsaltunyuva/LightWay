public class CircularLinkedList
{
    // Node class for Circular Linked List
    public class Node
    {
        public int count;
        public string name;
        public Node next;

        public Node(int count, string name)
        {
            this.count = count;
            this.name = name;
            this.next = null;
        }
    }

    // Head node of the Circular Linked List
    public Node head = null;

    // Method to add a node to the circular linked list
    public void AddNode(int count, string name)
    {
        Node newNode = new Node(count, name);

        if (head == null)
        {
            head = newNode;
            head.next = head; // Circular reference
        }
        else
        {
            Node temp = head;
            while (temp.next != head)
            {
                temp = temp.next;
            }

            temp.next = newNode;
            newNode.next = head;
        }
    }

    // Method to remove a node by name
    public bool RemoveNode(string name)
    {
        if (head == null)
        {
            return false; // List is empty
        }

        Node temp = head;
        Node prev = null;

        do
        {
            if (temp.name == name)
            {
                if (prev == null) // Node to remove is head
                {
                    // Find the last node and make it point to the new head
                    Node lastNode = head;
                    while (lastNode.next != head)
                    {
                        lastNode = lastNode.next;
                    }

                    if (head.next == head) // Only one node in the list
                    {
                        head = null;
                    }
                    else
                    {
                        head = head.next;
                        lastNode.next = head;
                    }
                }
                else
                {
                    prev.next = temp.next;
                }

                return true;
            }

            prev = temp;
            temp = temp.next;
        } while (temp != head);

        return false; // Node with the given name not found
    }

    // Method to get the count and name of a node by position
    public (int, string) GetNodeData(int position)
    {
        if (head == null || position < 0)
        {
            return (-1, null); // Invalid position or empty list
        }

        Node temp = head;
        int currentIndex = 0;

        do
        {
            if (currentIndex == position)
            {
                return (temp.count, temp.name);
            }

            temp = temp.next;
            currentIndex++;
        } while (temp != head);

        return (-1, null); // Position out of bounds
    }

    // Method to set the count and name of a node at a given position
    public bool SetNodeData(int position, int newCount, string newName)
    {
        if (head == null || position < 0)
        {
            return false; // Invalid position or empty list
        }

        Node temp = head;
        int currentIndex = 0;

        do
        {
            if (currentIndex == position)
            {
                temp.count = newCount;
                temp.name = newName;
                return true;
            }

            temp = temp.next;
            currentIndex++;
        } while (temp != head);

        return false; // Position out of bounds
    }

    // Method to check if the list is empty
    public bool IsEmpty()
    {
        return head == null;
    }

    // Method to get the size of the circular linked list
    public int GetSize()
    {
        if (head == null)
        {
            return 0; // List is empty
        }

        int size = 0;
        Node temp = head;

        do
        {
            size++;
            temp = temp.next;
        } while (temp != head);

        return size;
    }
}
