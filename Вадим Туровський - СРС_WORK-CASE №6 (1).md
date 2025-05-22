## 1. Installing additional command interpreters:

Suppose a user wants to install `zsh` and `fish` in addition to `bash`.

* **Installation:**

    ```bash
    sudo apt update  # Update the package list (for Debian/Ubuntu)
    sudo apt install zsh fish # Install zsh and fish (for Debian/Ubuntu)

    # For Fedora/CentOS/RHEL use dnf or yum respectively:
    # sudo dnf install zsh fish
    # sudo yum install zsh fish
    ```

* **Description of capabilities:**
    * **Zsh (Z Shell):** An extension of the standard `bash`. It has many useful features such as improved auto-completion (not only commands and files, but also options), spelling error correction, powerful themes, plugins (e.g., for git), and much more. Zsh is considered more interactive and convenient for experienced users.
    * **Fish (Friendly Interactive Shell):** Designed with a focus on ease of use. It has excellent out-of-the-box auto-completion (with highlighting and suggestions), syntax highlighting, autosuggestions based on command history, and a clear scripting syntax (although it differs from POSIX sh/bash). Fish is often chosen for its visual appeal and ease of learning.

## 2. Creating new users and groups:

We will create the necessary groups and then add users to them.

* **Creating groups:**

    ```bash
    sudo groupadd technical_support
    sudo groupadd developers
    sudo groupadd financiers
    sudo groupadd founders
    sudo groupadd guests
    ```

* **Creating users and adding them to groups:**

    ```bash
    sudo useradd -m -g technical_support tech1
    sudo useradd -m -g technical_support tech2

    sudo useradd -m -g developers dev1
    sudo useradd -m -g developers dev2
    sudo useradd -m -g developers dev3

    sudo useradd -m -g financiers fin1
    sudo useradd -m -g financiers fin2

    sudo useradd -m -g founders founder1
    sudo useradd -m -g founders founder2

    sudo useradd -m -g guests guest1
    ```

    The `-m` option creates a home directory for the user, and `-g` specifies the primary group. You will also need to set passwords for these users using the command `sudo passwd <username>`.

## 3. Setting the default command interpreter:

For each user, we will change their default shell. The user's shell information is stored in the `/etc/passwd` file.

* **Technical support – bash:** Usually, new users get `bash` by default, so nothing additional needs to be done for `tech1` and `tech2` if your system is configured that way. If not, you can explicitly set `bash`:

    ```bash
    sudo usermod -s /bin/bash tech1
    sudo usermod -s /bin/bash tech2
    ```

* **Developers – zsh:**

    ```bash
    sudo usermod -s /usr/bin/zsh dev1
    sudo usermod -s /usr/bin/zsh dev2
    sudo usermod -s /usr/bin/zsh dev3
    ```

    (Make sure the path to `zsh` is correct, usually it's `/usr/bin/zsh` or `/bin/zsh`).

* **Financiers – deny access to command interpreters:** To deny shell access, `/usr/sbin/nologin` is usually used.

    ```bash
    sudo usermod -s /usr/sbin/nologin fin1
    sudo usermod -s /usr/sbin/nologin fin2
    ```

* **Founders – fish:**

    ```bash
    sudo usermod -s /usr/bin/fish founder1
    sudo usermod -s /usr/bin/fish founder2
    ```

    (Make sure the path to `fish` is correct).

* **Guests – deny access to command interpreters:**

    ```bash
    sudo usermod -s /usr/sbin/nologin guest1
    ```

## 4. Demonstrating the work of each user group:

To demonstrate the work, you will need to switch to each user (for example, using `su <username>`) and execute some commands.

* **Technical support (bash):**

    ```bash
    su tech1
    uname -a        # Kernel information
    df -h           # Disk space usage
    date            # Current date and time
    pwd             # Current working directory
    exit
    ```

* **Developers (zsh):**

    ```bash
    su dev1
    uname -a
    df -h
    date
    pwd
    exit
    ```

* **Financiers (nologin):**
    Trying to log in as a user with `/usr/sbin/nologin` as their shell will result in a "This account is currently not available." message. For example, the command `su fin1` should output this message.
* **Founders (fish):**

    ```bash
    su founder1
    uname -a
    df -h
    date
    pwd
    exit
    ```

* **Guests (nologin):**
    Similar to `financiers`, trying `su guest1` should fail with a message indicating that the account is not available.

**Execution principle:**

1.  **Installing shell:** Package managers (`apt`, `dnf`, `yum`) are used to download and install the necessary command interpreters.
2.  **User and group management:** The `groupadd` command is used to create new groups, and `useradd` to create new users, specifying the primary group.
3.  **Changing the default shell:** The `usermod -s <shell>` command changes the shell that is launched for the user upon login. Denying access is achieved by setting `/usr/sbin/nologin` as the shell.
4.  **Demonstration:** Switching between users (`su`) allows executing commands in the context of each user and demonstrating the features of their shell or the lack of access to it.
