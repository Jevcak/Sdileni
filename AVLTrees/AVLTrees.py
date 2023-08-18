# class representing a tree node
class Node:
    def __init__(self, key):
        self.key = key
        self.left = None
        self.right = None
        self.height = 1
#function that returns the height of a node
def getHeight(root):
    if not root:
        return 0
    else:
        return root.height
# function that returns the difference between heights of subtrees
# balance = RightSubTreeHeight-LeftSubTreeHeight
def getBalance(root):
    if not root:
        return 0
    else:
        return getHeight(root.right) - getHeight(root.left)
# returns the node with minimal value from the tree 
def getMinNode(root):
    if not root.left:
        return root
    else:
        return getMinNode(root.left)
# class representing the tree
class Tree:
    def __init__(self):
        self.count = 0
# function to insert into tree that also balances the tree
def insert(tree, root, key):
    # if the key value is not found in the tree we add it
    if not root:
        tree.count+=1
        return Node(key)
    # if the key value is already in the tree we don't add duplicates
    elif root.key == key:
        return root
    # if the key value is smaller that the value of the node we are in 
    # we go and insert the value into the left subtree
    elif root.key > key:
        root.left = insert(tree, root.left, key)
    # if the key value is bigger, than we insert it into the right subtree
    else:
        root.right = insert(tree, root.right, key)
    # we update the height of the node we are currently in
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    # we check the balance
    # and if the absolute value of the balance factor is greater than 1 
    # then the condition for AVL trees isn't satisfied and we must 
    # rotate the tree
    balance = getBalance(root)
    # if balance is greater than 1, it means that the right subtree is higher
    if balance > 1:
        # if the inserted key is greater than the value of the right son of the current root
        # we know, that the signal is coming from the right son of the right son of the current root
        # and we can fix this by one left rotation on the root
        if key > root.right.key:
            return RotationL(root)
        # if the signal is coming from the left son of the right son of the current root, then
        # we must do a double rotation, firstly a right rotation on the right son of the root 
        # and secondly a left rotation on the root
        else:
            root.right = RotationR(root.right)
            return RotationL(root)
    # if balance is smaller than -1, it means that the left subtree is higher
    if balance < -1:
        # to check which rotation to do we use a similar principle to the one used when balance>1
        if key < root.left.key:
            return RotationR(root)
        else:
            root.left = RotationL(root.left)
            return RotationR(root)
    # we return the current root
    return root

def delete(tree, root, key):
    # if root is None we return it, because the value 
    # we are trying to delete doesn't exist in the tree
    if not root:
        return root
    # if the value we are trying to delete is greater than that of current node 
    # we recursively delete the value in the right subtree
    elif root.key < key:
        root.right = delete(tree, root.right, key)
    # if the value to be deleted is smaller, then we 
    # recursively try to delete it in the left subtree
    elif root.key > key:
        root.left = delete(tree, root.left, key)
    # the else branch represents when we find the node with the value we want to delete
    else:
        tree.count-=1
        # if the node we want to delete doesn't have a left or a right son, we just 
        # connect the existing son instead of the current node
        if not root.left:
            x = root.right
            root = None
            return x
        elif not root.right:
            x = root.left
            root = None
            return x
        # if the node has both sons
        # instead of the deleted node we use the lowest value node from the right subtree
        # so it satisfies BST condition
        else:
            x = getMinNode(root.right)
            root.key = x.key
            root.right = delete(tree,root.right, x.key)
    # if the root had no sons, then it has been labeled as None and we can return it
    if root is None:
            return root
    # we update the height of the current node
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    # we get the balance
    balance = getBalance(root)
    # if the balance is greater than 1, then the deleted node was in the left subtree and we 
    # need to rotate the right subtree, to satisfy the balance factor
    if balance > 1:
        # if the balance of the right son of root is greater than 0
        # then we can use simple rotation to fix the heights
        if getBalance(root.right) >= 0:
            return RotationL(root)
        # if it's smaller then we need to do double rotation
        else:
            root.right = RotationR(root.right)
            return RotationL(root)
    # if balance is smaller than -1, the process is similar to balance > 1
    if balance < -1:
        if getBalance(root.left) <= 0:
            return RotationR(root)
        else:
            root.left = RotationL(root.left)
            return RotationR(root)
    # we return the current roots
    return root
# function to determine if a value is stored in a tree
def find(tree, root, key):
    # if the desired value doesn't exist in the tree, we return False
    if not root:
        return False
    # we recursively go through the tree according to the BST principles
    elif root.key < key:
        return find(tree, root.right, key)
    elif root.key > key:
        return find(tree,root.left, key)
    # if the key of the root matches the value, we return True
    else:
        return True

# recursive function to print tree in preOrder
def preOrder(root):
    if not root:
        return
    print(root.key,end=" ")
    preOrder(root.left)
    preOrder(root.right)
    return
# recursive function to get ordered array of the values of the tree
def inOrder(root, temp):
    if not root:
        return temp
    temp = inOrder(root.left, temp)
    temp.append(root.key)
    temp = inOrder(root.right, temp)
    return temp

# function to rotate an edge to the left
# as a parameter it takes the higher vertex of the edge
def RotationL(root):
    y = root.right
    B = y.left
    y.left = root
    root.right = B
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    y.height = 1 + max(getHeight(y.left),getHeight(y.right))
    return y

# function to rotate an edge to the right
# as a parameter it takes the higher vertex of the edge
def RotationR(root):
    y = root.left
    B = y.right
    y.right = root
    root.left = B
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    y.height = 1 + max(getHeight(y.left),getHeight(y.right))
    return y

# function to merge two sorted arrays into one, it takes only one copy of each number
def merge(array1, array2):
    i,j = 0,0
    m = len(array1)
    n = len(array2)
    merged = []
    while i < m and j < n:
        if array1[i] < array2[j]:
            merged.append(array1[i])
            i+=1
        elif array1[i] == array2[j]:
            merged.append(array1[i])
            i+=1
            j+=1
        else:
            merged.append(array2[j])
            j+=1
    if i == m:
        while j < n:
            merged.append(array2[j])
            j+=1
    else:
        while i < m:
            merged.append(array1[i])
            i+=1
    return merged

# recursive function that constructs a tree from a sorted array
def constructTree(tree, root, array):
    # if there is no array, it returns None because the root doesn't exist
    if not array:
        return None
    # it always splits the array and uses the middle number as root
    # after, that it calls itself to construct the left subtree and then the right subtree
    n = len(array) // 2
    root = Node(array[n])
    root.left = constructTree(tree, root.left, array[:n])
    root.right = constructTree(tree, root.right, array[n+1:])
    tree.count+=1
    # we return the root
    return root

# function to recursively delete all nodes in a tree
def deleteNodes(root):
    if not root:
        return None
    root.left = deleteNodes(root.left)
    root.right = deleteNodes(root.right)
    root = None 
    return root

#function, that deletes the keys from dictionaries and calls the function to delete the nodes
def deleteTree(key):
    deleteNodes(roots[key])
    roots.pop(key)
    trees[key] = None
    trees.pop(key)

trees = {}
roots = {}
#limitation for printing the tree
limit = 256
# this part reads the 'AVL.txt' file and calls functions accordingly
file = open('AVL.txt', 'r')
while True:
    line = file.readline()
    if not line:
        break
    l = line.split()
    # l[0] is the name of operation
    if l[0] == 'insert':
        # l[1] is the key for the root and tree and the name of the tree given by user
        if not l[1] in trees:
            newTree = Tree()
            trees[l[1]] = newTree
            roots[l[1]] = None
        for num in l[2:]:
            roots[l[1]] = insert(trees[l[1]], roots[l[1]], int(num))
        if trees[l[1]].count > limit:
            print('{} obsahuje o {} vice vrcholu nez je mozne vypsat'.format(l[1],trees[l[1]].count-limit ), end=' ')
    elif l[0] == 'delete':
        for num in l[2:]:
            roots[l[1]] = delete(trees[l[1]], roots[l[1]], int(num))
        if not roots[l[1]]:
            deleteTree(l[1])
    elif l[0] == 'find':
        for num in l[2:]:
            print(find(trees[l[1]], roots[l[1]], int(num)))
    elif l[0] == 'merge':
        # l[3] is the name of the new tree
        if not l[3] in trees:
            newTree = Tree()
            trees[l[3]] = newTree
            roots[l[3]] = None
        temp = []
        arr1 = inOrder(roots[l[1]], temp)
        temp = []
        arr2 = inOrder(roots[l[2]], temp)
        arr3 = merge(arr1, arr2)
        roots[l[3]] = constructTree(trees[l[3]],roots[l[3]],arr3)
        arr1,arr2,arr3 = [],[],[]
    elif l[0] == 'print':
        for key in l[1:]:
            if key in roots:
                if trees[key].count <= limit:
                    preOrder(roots[key])
                else:
                    print('{} obsahuje vice vrcholu nez je povoleno vypsat'.format(key), end=' ')
            else:
                print('Strom {} neexistuje'.format(key), end=' ')
            print()
    elif l[0] == 'destroy':
        for key in l[1:]:
            if key in roots:
                deleteTree(key)