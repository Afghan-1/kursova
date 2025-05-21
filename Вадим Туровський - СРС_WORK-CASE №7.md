**1. В ході роботи досить часто виникає завдання планування задач:**

  * **Main functions of a task scheduler:**

      * **Automation of execution:** Launching programs, scripts, or commands at a specific time or upon certain events without user intervention.
      * **Periodic execution:** Running tasks at defined intervals (minutes, hours, days, weeks, months).
      * **Scheduled execution:** Running tasks at a specific time and date.
      * **Event-driven execution:** Running tasks in response to certain system events (e.g., system startup, user login/logout).
      * **Execution management:** Ability to view scheduled tasks, their status, execution history, as well as editing and deleting them.

  * **Comparison of task scheduling in Windows and Linux:**

      * **Flexibility of triggers:** Windows Task Scheduler offers a wider range of triggers, including not only time and date but also system events such as program startup, log events, user inactivity time, etc. Cron in Linux is more focused on time-based triggers.
      * **Task management:** Both systems provide capabilities for managing scheduled tasks (viewing, editing, deleting, manual execution). Windows Task Scheduler can provide detailed information about the execution status of tasks.
      * **Extensibility:** In Linux, in addition to Cron, there are other schedulers such as systemd Timers, which are integrated with systemd and offer more advanced features similar to Windows Task Scheduler triggers.

  * **Basic principles of working with the Cron scheduler in Linux OS:**

    Cron uses a configuration file called "crontab" (cron table) to store a list of scheduled tasks and their execution times. Each user can have their own crontab, and there is also a system crontab (usually located in `/etc/crontab`).

    A line in crontab consists of six fields separated by spaces or tabs:

    ```
    minute hour day_of_month month day_of_week command
    ```

    Where:

      * minute: Minute (0-59)
      * hour: Hour (0-23)
      * day\_of\_month: Day of the month (1-31)
      * month: Month (1-12 or month names)
      * day\_of\_week: Day of the week (0-6 or day names, 0 = Sunday)
      * command: Command or script to execute

  * **Cron Configuration:**

      * **Editing crontab:** To edit your own crontab, use the command:

        ```bash
        crontab -e
        ```

        You will be opened in a text editor (usually nano or vi) where you can add or modify lines with task schedules.

      * **Viewing crontab:** To view your current crontab, use the command:

        ```bash
        crontab -l
        ```

      * **Deleting crontab:** To delete your crontab, use the command:

        ```bash
        crontab -r
        ```

  * **Alternatives to Cron in Linux:**

      * **at:** Allows you to schedule a one-time execution of a command at a specified time. For example:

        ```bash
        echo "shutdown -h now" | at 23:00
        ```

        Characteristic: Simple for one-off tasks, less convenient for periodic tasks.

      * **systemd Timers:** Are part of systemd and offer more flexible scheduling capabilities, similar to Windows Task Scheduler. Timers can be configured based on time, events (e.g., system boot, network status change), etc. Configuration is done through unit files (.timer and .service).
        Characteristic: More powerful and integrated with the system, supports calendar events, startup events, dependencies. Can be more complex to configure for simple periodic tasks compared to Cron.

      * **anacron:** Designed for systems that are not running 24/7. It runs scheduled jobs with a delay if the system was powered off at the scheduled time.
        Characteristic: Ensures task execution even if the system was turned off, focused on daily, weekly, and monthly tasks.

**2. Для вашої віртуальної машини зі встановленою ОС Linux здійсніть планування обраних вами задач (запуск додатків, вмикання/вимикання машини, очистка каталогів, видалення файлів, резервне копіювання, архівування тощо на ваш вибір) через планувальник Cron:**

Assuming you are using standard Cron. Edit your crontab using `crontab -e` and add the following lines (replace the commands with those you want to execute):

  * **Executing a task at a specific time (8 AM):**

    ```
    0 8 * * * echo "Running morning task" >> /home/your_user/cron.log
    ```

  * **Executing a task at 6:30 PM:**

    ```
    30 18 * * * echo "Running evening task" >> /home/your_user/cron.log
    ```

  * **Executing the same task twice a day (9:00 AM and 3:00 PM):**

    ```
    0 9,15 * * * echo "Running task twice a day" >> /home/your_user/cron.log
    ```

  * **Executing a task only on weekdays (from 8 AM to 6 PM, for example at 12:00 PM):**

    ```
    0 12 * * 1-5 echo "Running weekday task" >> /home/your_user/cron.log
    ```

  * **Executing tasks:**

      * **Once a year (January 1st at 00:00):**

        ```
        0 0 1 1 * echo "Annual task" >> /home/your_user/cron.log
        ```

      * **Once a month (on the 1st at 01:00):**

        ```
        0 1 1 * * echo "Monthly task" >> /home/your_user/cron.log
        ```

      * **Once a day (at 02:00):**

        ```
        0 2 * * * echo "Daily task" >> /home/your_user/cron.log
        ```

      * **Every hour (at the beginning of each hour):**

        ```
        0 * * * * echo "Hourly task" >> /home/your_user/cron.log
        ```

      * **On startup (after reboot) - using `@reboot`:**

        ```
        @reboot echo "Task after reboot" >> /home/your_user/reboot.log
        ```

Just remember to replace `echo "..." >> ...log` with the actual commands you want to schedule (e.g., running a program, a backup script, etc.).

**3\. Встановіть альтернативний Cron’у планувальник задач (на Ваш вибір). Виконані у завданні 2 дії продемонструйте через нього.** 

**p.s** Нажаль не можу це завдання виконати, але хоч описав принцип до 3 завдання.

